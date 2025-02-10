using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Application.Contracts.Identity;
using Ecommerce.Application.Exceptions;
using Ecommerce.Application.Features.Orders.Vms;
using Ecommerce.Application.Models.Payment;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Stripe;

namespace Ecommerce.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderVm>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly UserManager<Usuario> _userManager;
        private readonly StripeSettings _stripeSettings;

        public CreateOrderCommandHandler(IUnitOfWork unitOfWork, IAuthService authService, IMapper mapper, UserManager<Usuario> userManager, IOptions<StripeSettings> stripeSettings)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
            _mapper = mapper;
            _userManager = userManager;
            _stripeSettings = stripeSettings.Value;
        }

        public async Task<OrderVm> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {

            //obtener el username de sesión y usarlo para buscar una orden que coincida con el username y cuyo status sea pending.

            var username = _authService.GetSessionUser();

            var orderPending = await _unitOfWork.Repository<Order>().GetEntityAsync(
                x => x.CompradorUsername == username && x.Status == OrderStatus.Pending,
                null,
                true
            );

            if (orderPending is not null)
            {
                await _unitOfWork.Repository<Order>().DeleteAsync(orderPending);
            }

            //crear una lista que incluya los items dentro del shoppingCart

            var includes = new List<Expression<Func<ShoppingCart, object>>>();
            includes.Add(x => x.ShoppingCartItems!.OrderBy(y => y.Producto));

            //buscar el shoppingCart en la Db con el ShoppingCartId proveniente del request y traer también la lista de ShoppingCartItems asociados a ese ShoppingCart

            var shoppingCart = await _unitOfWork.Repository<ShoppingCart>().GetEntityAsync(
                x => x.ShoppingCartMasterId == request.ShoppingCartId,
                includes,
                false);

            if (shoppingCart == null)
            {
                throw new NotFoundException(nameof(ShoppingCart), request.ShoppingCartId);
            }

            //buscar el usuario en la base de datos usando el username de sessión.

            var user = await _userManager.FindByNameAsync(username);

            if (user is null)
            {
                throw new Exception("El usuario no está autenticado");
            }

            //buscar la dirección del usuario en la base de datos cuyo username coincida con el username de usuario en sesión.

            var userAddress = await _unitOfWork.Repository<Domain.Address>().GetEntityAsync(
                x => x.Username == username,
                null,
                false);

            //crear un objeto OrderAddres con los datos obtenidos de la consulta anterior de la base de datos

            OrderAdress orderAddress = new OrderAdress
            {
                Direccion = userAddress.Direccion,
                Ciudad = userAddress.Ciudad,
                CodigoPostal = userAddress.CodigoPostal,
                Pais = userAddress.Pais,
                Departamento = userAddress.Departamento,
                Username = userAddress.Username

            };

            await _unitOfWork.Repository<OrderAdress>().AddAsync(orderAddress);

            //obtener los datos del shoppingCart para poder crear un objeto Order.

            var subtotal = Math.Round(shoppingCart.ShoppingCartItems!.Sum(x => x.Precio * x.Cantidad), 2);
            var impuesto = Math.Round(subtotal * Convert.ToDecimal(0.18), 2);
            var precioEnvio = subtotal < 100 ? 10 : 25;
            var total = subtotal + impuesto + precioEnvio;

            var nombreComprador = $"{user.Nombre} {user.Apellido}";

            //Crear objeto Order con los datos previamente obtenidos.

            var order = new Order(
                nombreComprador,
                user.UserName!,
                orderAddress,
                subtotal,
                total,
                impuesto,
                precioEnvio
            );

            await _unitOfWork.Repository<Order>().AddAsync(order);

            //Crear una lista de OrderItem

            var items = new List<OrderItem>();

            //Por cada producto dentro del atributo ShoppingCartItems de tipo List, crear un objeto OrderItem con esos datos.

            foreach (var product in shoppingCart.ShoppingCartItems!)
            {
                var orderItem = new OrderItem
                {
                    ProductNombre = product.Producto,
                    ProductId = product.ProductId,
                    ImagenUrl = product.Imagen,
                    Precio = product.Precio,
                    Cantidad = product.Cantidad,
                    OrderId = order.Id


                };
                items.Add(orderItem);
            }

            //Agregar estos objetos a la base de datos

            _unitOfWork.Repository<OrderItem>().AddRange(items);

            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                throw new Exception("Error creando la orden de compra");
            }

            //Configurar Stripe con los datos provenientes del StripeSettings y con el SDK de Stripe

            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;

            var stripePaymentService = new PaymentIntentService();
            PaymentIntent intent;

            //Si no hay un PaymentIntentId entonces creamos uno nuevo

            if (string.IsNullOrEmpty(order.PaymentIntentId))
            {
                //Crear options para crear un Intent de pago

                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)order.Total,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }

                };

                //Crear el Intent de pago

                intent = await stripePaymentService.CreateAsync(options);

                //Añadir los datos del Intent de Pago al objeto Order

                order.PaymentIntentId = intent.Id;
                order.ClientSecret = intent.ClientSecret;
                order.StripeApiKey = _stripeSettings.PublishbleKey;
            }
            else
            {
                //Si ya hay un order.PaymentIntentId, actualizar el monto.

                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)order.Total,
                };

                //actualizar el paymentIntent con la nueva data (amount)
                await stripePaymentService.UpdateAsync(order.PaymentIntentId, options);
            }

            //Actualizar el registro de la orden creada en la base de datos con los datos de Stripe.

            _unitOfWork.Repository<Order>().UpdateEntity(order);
            var resultUpdateOrder = await _unitOfWork.Complete();

            if (resultUpdateOrder <= 0)
            {
                throw new Exception("Error creando el payment intent");
            }

            //Mapear este objeto Order a un objeto de tipo OrderVm.

            var mappedOrder = _mapper.Map<OrderVm>(order);


            return mappedOrder;

        }
    }
}
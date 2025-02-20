import React, { Fragment, useEffect } from "react";
import MetaData from "../layout/MetaData";
import { CheckoutSteps } from "./CheckoutSteps";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import { useAlert } from "react-alert";
import { resetUpdateStatus } from "../../slices/orderSlice";
import { saveOrder } from "../../actions/orderAction";

const ConfirmOrder = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const alert = useAlert();

  const {
    shoppingCartId,
    shoppingCartItems,
    total,
    cantidad,
    subtotal,
    precioEnvio,
    impuesto,
  } = useSelector((state) => state.shoppingCart);

  const items = shoppingCartItems.slice();

  const { isUpdated, errores } = useSelector((state) => state.order);

  const { user, direccionEnvio } = useSelector((state) => state.security);

  useEffect(() => {
    if (isUpdated) {
      navigate("/payment");
      dispatch(resetUpdateStatus({}));
    }

    if (errores) {
      errores.map((error) => alert.error(error));
    }
  }, [dispatch, errores, isUpdated, navigate]);

  const sendOrderSubmitHandler = () => {
    const request = {
      shoppingCartId,
    };

    dispatch(saveOrder(request));
  };

  return (
    <Fragment>
      <MetaData titulo={"Confirm Order"} />
      <CheckoutSteps envio confirmacion />

      <div class="row d-flex justify-content-between">
        <div class="col-12 col-lg-8 mt-5 order-confirm">
          <h4 class="mb-3">Shipping Info</h4>
          <p>
            <b>Name:</b> {user.nombre}
          </p>
          <p>
            <b>Phone:</b> {user.telefono}
          </p>
          <p class="mb-4">
            <b>Address:</b>
            {(direccionEnvio ? direccionEnvio.direccion : "") +
              ", " +
              (direccionEnvio ? direccionEnvio.ciudad : "") +
              ", " +
              (direccionEnvio ? direccionEnvio.departamento : "") +
              ", " +
              (direccionEnvio ? direccionEnvio.codigoPostal : "") +
              ", " +
              (direccionEnvio ? direccionEnvio.pais : "")}{" "}
          </p>

          <hr />
          <h4 class="mt-4">Your Cart Items:</h4>

          {shoppingCartItems.map((item) => {
            <Fragment key={item.id}>
              <hr />
              <div class="cart-item my-1">
                <div class="row">
                  <div class="col-4 col-lg-2">
                    <img
                      src={item.imagen}
                      alt={item.producto}
                      height="45"
                      width="65"
                    />
                  </div>

                  <div class="col-5 col-lg-6">
                    <Link to={`/product/${item.productId}`}>
                      {item.producto}
                    </Link>
                  </div>

                  <div class="col-4 col-lg-4 mt-4 mt-lg-0">
                    <p>
                      {item.cantidad} x {item.precio} =
                      {<b>${item.totalLine}</b>}
                    </p>
                  </div>
                </div>
              </div>
              <hr />
            </Fragment>;
          })}
        </div>

        <div class="col-12 col-lg-3 my-4">
          <div id="order_summary">
            <h4>Order Summary</h4>
            <hr />
            <p>
              Subtotal: <span class="order-summary-values">${subtotal}</span>
            </p>
            <p>
              Shipping: <span class="order-summary-values">${precioEnvio}</span>
            </p>
            <p>
              Tax: <span class="order-summary-values">${impuesto}</span>
            </p>

            <hr />

            <p>
              Total: <span class="order-summary-values">${total}</span>
            </p>

            <hr />
            <button
              id="checkout_btn"
              class="btn btn-primary btn-block"
              onClick={sendOrderSubmitHandler}
            >
              Proceed to Payment
            </button>
          </div>
        </div>
      </div>
    </Fragment>
  );
};

export default ConfirmOrder;

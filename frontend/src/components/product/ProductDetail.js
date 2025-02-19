import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useParams } from "react-router-dom";
import { getProductById } from "../../actions/productAction";
import { useAlert } from "react-alert";
import { Loader } from "../layout/Loader";
import { Carousel } from "react-bootstrap";
import { addShoppingCartItem } from "../../actions/cartAction";

const ProductDetail = () => {
  const { shoppingCartId, shoppingCartItems } = useSelector(
    (state) => state.shoppingCart
  );

  const dispatch = useDispatch();
  const alert = useAlert();
  const { id } = useParams();
  const { loading, error, product } = useSelector((state) => state.product);
  const [quantity, setQuantity] = useState(1);

  useEffect(() => {
    dispatch(getProductById(id));

    if (error != null) {
      alert.error(error);
    }
  }, [dispatch, alert, error, id]);

  if (!product || loading) {
    return <Loader />;
  }

  const increaseQty = () => {
    const count = document.querySelector(".count");
    if (count.valueAsNumber >= product.stock) {
      return;
    }

    const qty = count.valueAsNumber + 1;
    setQuantity(qty);
  };
  const decreaseQty = () => {
    const count = document.querySelector(".count");

    if (count.valueAsNumber <= 1) {
      return;
    }

    const qty = count.valueAsNumber - 1;
    setQuantity(qty);
  };

  const addProductToCart = () => {
    const item = {
      cantidad: quantity,
      imagen: product.images[0].url,
      precio: product.precio,
      productId: product.id,
      producto: product.nombre,
      stock: product.stock,
    };
    console.log(item);

    const params = {
      cantidad: quantity,
      productId: id,
      shoppingCartItems,
      shoppingCartId,
      item,
    };
    console.log(params);

    dispatch(addShoppingCartItem(params));
    alert.success("El producto se agrego al carrito correctamente");
  };

  return (
    <div className="row f-flex justify-content-around">
      <div className="col-12 col-lg-5 img-fluid" id="product_image">
        <Carousel pause="hover">
          {product.images.map((image) => (
            <Carousel.Item key={image.id}>
              <img
                className="d-block w100"
                src={image.url}
                alt={product.nombre}
              />
            </Carousel.Item>
          ))}
        </Carousel>
      </div>

      <div className="col-12 col-lg-5 mt-5">
        <h3>{product.nombre}</h3>
        <p id="product_id">Product # {product.id}</p>

        <hr />

        <div className="rating-outer">
          <div
            className="rating-inner"
            style={{ width: `${(product.rating / 5) * 100}%` }}
          ></div>
        </div>
        <span id="no_of_reviews">({product.numeroReviews} Reviews)</span>

        <hr />

        <p id="product_price">{product.precio}</p>

        <div className="stockCounter d-inline">
          <span className="btn btn-danger minus" onClick={decreaseQty}>
            -
          </span>

          <input
            type="number"
            className="form-control count d-inline"
            value={quantity}
            onChange={(e) => setQuantity(e.target.value)}
            readOnly
          />

          <span className="btn btn-primary plus" onClick={increaseQty}>
            +
          </span>
        </div>

        <button
          type="button"
          id="cart_btn"
          className="btn btn-primary d-inline ml-4"
          onClick={addProductToCart}
        >
          Add to Cart
        </button>

        <hr />

        <p>
          Status:{" "}
          <span
            id="stock_status"
            className={product.stock > 0 ? "greenColor" : "redColor"}
          >
            {product.stock > 0 ? "En Stock" : "Fuera de Stock"}
          </span>
        </p>

        <hr />

        <h4 className="mt-2">Description:</h4>
        <p>{product.descripcion}</p>
        <hr />
        <p id="product_seller mb-3">
          Vendido por: <strong>{product.vendedor}</strong>
        </p>

        <button
          id="review_btn"
          type="button"
          className="btn btn-primary mt-4"
          data-toggle="modal"
          data-target="#ratingModal"
        >
          Submit Your Review
        </button>

        <div className="row mt-2 mb-5">
          <div className="rating w-50">
            <div
              className="modal fade"
              id="ratingModal"
              tabIndex="-1"
              role="dialog"
              aria-labelledby="ratingModalLabel"
              aria-hidden="true"
            >
              <div className="modal-dialog" role="document">
                <div className="modal-content">
                  <div className="modal-header">
                    <h5 className="modal-title" id="ratingModalLabel">
                      Submit Review
                    </h5>
                    <button
                      type="button"
                      className="close"
                      data-dismiss="modal"
                      aria-label="Close"
                    >
                      <span aria-hidden="true">&times;</span>
                    </button>
                  </div>
                  <div className="modal-body">
                    <ul className="stars">
                      <li className="star">
                        <i className="fa fa-star"></i>
                      </li>
                      <li className="star">
                        <i className="fa fa-star"></i>
                      </li>
                      <li className="star">
                        <i className="fa fa-star"></i>
                      </li>
                      <li className="star">
                        <i className="fa fa-star"></i>
                      </li>
                      <li className="star">
                        <i className="fa fa-star"></i>
                      </li>
                    </ul>

                    <textarea
                      name="review"
                      id="review"
                      className="form-control mt-3"
                    ></textarea>

                    <button
                      className="btn my-3 float-right review-btn px-4 text-white"
                      data-dismiss="modal"
                      aria-label="Close"
                    >
                      Submit
                    </button>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default ProductDetail;

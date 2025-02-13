import React from "react";
import { Link } from "react-router-dom";

export const Product = ({ product, col }) => {
  const default_image = "./images/default_product.png";

  return (
    <div className={`col-sm-12 col-md-6 col-lg-${col} my-3`}>
      <div className="card p-3 rounded">
        <img
          className="card-img-top mx-auto"
          src={product.images[0] ? product.images[0].url : default_image}
        />
        <div className="card-body d-flex flex-column">
          <h5 className="card-title">
            <Link>{product.nombre}</Link>
          </h5>
          <div className="ratings mt-auto">
            <div className="rating-outer">
              <div
                className="rating-inner"
                style={{ width: `${(product.rating / 5) * 100}%` }}
              ></div>
            </div>
            <span id="no_of_reviews">({product.numeroReviews} Reviews)</span>
          </div>
          <p className="card-text">${product.precio}</p>
          <Link id="view_btn" className="btn btn-block">
            Ver Detalles
          </Link>
        </div>
      </div>
    </div>
  );
};

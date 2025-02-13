import React, { Fragment } from "react";

export const Home = () => {
  return (
    <Fragment>
      <section id="products" className="container mt-5">
        <div className="row">
          <div className="col-sm-12 col-md-6 col-lg-3 my-3">
            <div className="card p-3 rounded">
              <img
                className="card-img-top mx-auto"
                src="https://firebasestorage.googleapis.com/v0/b/ecommerce-e4e6c.appspot.com/o/images%2Fzapato8.jpg-1617998795201?alt=media&token=9e62c323-1a92-4be4-8507-34cee836d5ea"
              />
              <div className="card-body d-flex flex-column">
                <h5 className="card-title">
                  <a href="">128GB Solid Storage Memory card - SanDisk Ultra</a>
                </h5>
                <div className="ratings mt-auto">
                  <div className="rating-outer">
                    <div className="rating-inner"></div>
                  </div>
                  <span id="no_of_reviews">(5 Reviews)</span>
                </div>
                <p className="card-text">$45.67</p>
                <a href="#" id="view_btn" className="btn btn-block">
                  Ver Detalles
                </a>
              </div>
            </div>
          </div>
        </div>
      </section>
    </Fragment>
  );
};

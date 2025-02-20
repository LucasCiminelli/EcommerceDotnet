import React, { Fragment } from "react";
import MetaData from "../layout/MetaData";

const OrderSuccess = () => {
  return (
    <Fragment>
      <MetaData titulo={"Success"} />
      <div className="row justify-content-center">
        <div className="col-6 mt-5 text-center">
          <img
            className="my-5 img-fluid d-block mx-auto"
            src="/images/order_success.png"
            alt="compra exitosa"
            width={"200"}
            height={"200"}
          />

          <h2>Tu compra ha sido completada con exito</h2>
        </div>
      </div>
    </Fragment>
  );
};

export default OrderSuccess;

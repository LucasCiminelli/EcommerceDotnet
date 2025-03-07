import React from "react";
import { Link } from "react-router-dom";

export const CheckoutSteps = ({ envio, confirmacion, payment }) => {
  return (
    <div className="checkout-progress d-flex justify-content-center mt-5">
      {envio ? (
        <Link to={"shipping"} className="float-right">
          <div className="triangle2-active"></div>
          <div className="step active-step">Shipping</div>
          <div className="triangle-active"></div>
        </Link>
      ) : (
        <Link to={"#!"} disabled>
          <div className="triangle2-incomplete"></div>
          <div className="step incomplete">Envío</div>
          <div className="triangle-incomplete"></div>
        </Link>
      )}

      {confirmacion ? (
        <Link to={"/order/confirm"} className="float-right">
          <div className="triangle2-active"></div>
          <div className="step active-step">Confirm Order</div>
          <div className="triangle-active"></div>
        </Link>
      ) : (
        <Link to={"#!"} disabled>
          <div className="triangle2-incomplete"></div>
          <div className="step incomplete">Confirm Order</div>
          <div className="triangle-incomplete"></div>
        </Link>
      )}

      {payment ? (
        <Link to={"/payment"} className="float-right">
          <div className="triangle2-active"></div>
          <div className="step active-step">Payment</div>
          <div className="triangle-active"></div>
        </Link>
      ) : (
        <Link to={"#!"} disabled>
          <div className="triangle2-incomplete"></div>
          <div className="step incomplete">Payment</div>
          <div className="triangle-incomplete"></div>
        </Link>
      )}
    </div>
  );
};

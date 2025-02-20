import React, { Fragment } from "react";
import MetaData from "../layout/MetaData";
import { CheckoutSteps } from "./CheckoutSteps";
import { useNavigate } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import {
  CardCvcElement,
  CardExpiryElement,
  CardNumberElement,
  Elements,
  useElements,
  useStripe,
} from "@stripe/react-stripe-js";
import { confirmPayment } from "../../actions/orderAction";
import { loadStripe } from "@stripe/stripe-js";
import { useAlert } from "react-alert";

const options = {
  style: {
    base: {
      fontSize: "16px",
    },
    invalid: {
      color: "#9e2146",
    },
  },
};

const Wrapper = (props) => (
  <Elements stripe={loadStripe(localStorage.getItem("stripeapi"))}>
    <Payment {...props} />
  </Elements>
);
const Payment = (props) => {
  const navigate = useNavigate();
  const alert = useAlert();
  const dispatch = useDispatch();
  const stripe = useStripe();
  const elements = useElements();

  const { user } = useSelector((state) => state.security);
  const { clientSecret, order } = useSelector((state) => state.order);

  console.log(clientSecret);

  const { shoppingCartId } = useSelector((state) => state.shoppingCart);

  const submitHandler = async (e) => {
    e.preventDefault();
    document.querySelector("#pay_btn").disabled = true;

    try {
      if (!stripe || !elements) {
        return;
      }

      const result = await stripe.confirmCardPayment(clientSecret, {
        payment_method: {
          card: elements.getElement(CardNumberElement),
          billing_details: {
            name: `${user.nombre} ${user.apellido}`,
            email: user.email,
          },
        },
      });

      if (result.error) {
        alert.error(result.error.message);
        document.querySelector("#pay_btn").disabled = false;
      } else {
        if (result.paymentIntent.status === "succeeded") {
          const params = {
            orderId: order.id,
            shoppingCartMasterId: shoppingCartId,
          };

          dispatch(confirmPayment(params));
          navigate("/success");
        } else {
          alert.error("Errores en el procesamiento del pago");
        }
      }
    } catch (error) {
      document.querySelector("#pay_btn").disabled = false;
      alert.error(error);
    }
  };

  return (
    <Fragment>
      <MetaData titulo={"Payment"} />
      <CheckoutSteps envio confirmacion payment />

      <div className="row wrapper">
        <div className="col-10 col-lg-5">
          <form className="shadow-lg" onSubmit={submitHandler}>
            <h1 className="mb-4">Card Info</h1>

            <div className="form-group">
              <label htmlFor="card_num_field">Card Number</label>
              <CardNumberElement
                type="text"
                id="card_num_field"
                className="form-control"
                options={options}
              />
            </div>

            <div className="form-group">
              <label htmlFor="card_exp_field">Card Expiry</label>
              <CardExpiryElement
                type="text"
                id="card_exp_field"
                className="form-control"
                options={options}
              />
            </div>

            <div className="form-group">
              <label htmlFor="card_cvc_field">Card CVC</label>
              <CardCvcElement
                type="text"
                id="card_cvc_field"
                className="form-control"
                options={options}
              />
            </div>

            <button id="pay_btn" type="submit" className="btn btn-block py-3">
              Pay
            </button>
          </form>
        </div>
      </div>
    </Fragment>
  );
};

export default Wrapper;

import React, { Fragment, useEffect, useState } from "react";
import MetaData from "../layout/MetaData";
import { useAlert } from "react-alert";
import { useDispatch, useSelector } from "react-redux";
import { resetErrors } from "../../slices/forgotPasswordSlice";
import { forgotPassword } from "../../actions/userAction";

export const ForgotPassword = () => {
  const [email, setEmail] = useState("");
  const alert = useAlert();
  const dispatch = useDispatch();
  const { errores, message, loading } = useSelector(
    (state) => state.forgotPassword
  );

  useEffect(() => {
    if (errores) {
      errores.map((error) => alert.error(error));
      dispatch(resetErrors);
    }

    if (message) {
      alert.success(message);
    }
  }, [errores, alert, dispatch, message]);

  const submitHandler = (e) => {
    e.preventDefault();

    dispatch(forgotPassword({ email }));
  };

  return (
    <Fragment>
      <MetaData titulo={"Forgot Password"} />
      <div class="row wrapper">
        <div class="col-10 col-lg-5">
          <form class="shadow-lg" onSubmit={submitHandler}>
            <h1 class="mb-3">Forgot Password</h1>
            <div class="form-group">
              <label for="email_field">Enter Email</label>
              <input
                type="email"
                id="email_field"
                class="form-control"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
              />
            </div>

            <button
              id="forgot_password_button"
              type="submit"
              class="btn btn-block py-3"
              disabled={loading ? true : false}
            >
              Send Email
            </button>
          </form>
        </div>
      </div>
    </Fragment>
  );
};

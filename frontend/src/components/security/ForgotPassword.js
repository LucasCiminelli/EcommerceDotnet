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
      <div className="row wrapper">
        <div className="col-10 col-lg-5">
          <form className="shadow-lg" onSubmit={submitHandler}>
            <h1 className="mb-3">Forgot Password</h1>
            <div className="form-group">
              <label htmlFor="email_field">Enter Email</label>
              <input
                type="email"
                id="email_field"
                className="form-control"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
              />
            </div>

            <button
              id="forgot_password_button"
              type="submit"
              className="btn btn-block py-3"
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

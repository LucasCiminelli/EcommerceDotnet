import React, { useEffect, useState } from "react";
import { Fragment } from "react";
import MetaData from "../layout/MetaData";
import { useNavigate, useParams } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { useAlert } from "react-alert";
import { resetPassword } from "../../actions/userAction";
import { resetErrors } from "../../slices/resetPasswordSlice";

const NewPassword = () => {
  const navigate = useNavigate();
  const { token } = useParams(); //capturar el token desde la URL
  const dispatch = useDispatch();
  const alert = useAlert();

  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");

  const { errores, message, loading } = useSelector(
    (state) => state.resetPassword
  );

  useEffect(() => {
    if (errores) {
      errores.map((error) => alert.error(error));
      dispatch(resetErrors());
    }

    if (message) {
      alert.success(message);
      dispatch(resetErrors());
      navigate("/login");
    }
  }, [errores, alert, dispatch, message, navigate]);

  const submitHandler = (e) => {
    e.preventDefault();

    dispatch(resetPassword({ email, password, confirmPassword, token }));
  };

  return (
    <Fragment>
      <MetaData titulo={"Reset Password"} />
      <div className="row wrapper">
        <div className="col-10 col-lg-5">
          <form className="shadow-lg" onSubmit={submitHandler}>
            <h1 className="mb-3">Reset Password</h1>

            <div className="form-group">
              <label htmlFor="email_field">Email</label>
              <input
                type="email"
                id="email_field"
                className="form-control"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
              />
            </div>

            <div className="form-group">
              <label htmlFor="password_field">Password</label>
              <input
                type="password"
                id="password_field"
                className="form-control"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
              />
            </div>

            <div classNameName="form-group">
              <label htmlFor="confirm_password_field">Confirm Password</label>
              <input
                type="password"
                id="confirm_password_field"
                className="form-control"
                value={confirmPassword}
                onChange={(e) => setConfirmPassword(e.target.value)}
              />
            </div>

            <button
              id="new_password_button"
              type="submit"
              className="btn btn-block py-3"
              disabled={loading ? true : false}
            >
              Reset Password
            </button>
          </form>
        </div>
      </div>
    </Fragment>
  );
};

export default NewPassword;

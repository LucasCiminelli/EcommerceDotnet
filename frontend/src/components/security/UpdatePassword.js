import React, { Fragment, useEffect, useState } from "react";
import MetaData from "../layout/MetaData";
import { useNavigate } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { updatePassword } from "../../actions/userAction";
import { resetUpdateStatus } from "../../slices/securitySlice";
import { useAlert } from "react-alert";

const UpdatePassword = () => {
  const [oldPassword, setOldPassword] = useState("");
  const [newPassword, setNewPassword] = useState("");
  const { loading, errores, isUpdated } = useSelector(
    (state) => state.security
  );

  const navigate = useNavigate();
  const dispatch = useDispatch();
  const alert = useAlert();

  useEffect(() => {
    if (errores) {
      errores.map((error) => alert.error(error));
    }
    if (isUpdated) {
      alert.success("El password se actualizó correctamente");
      navigate("/me");
      dispatch(resetUpdateStatus({}));
    }
  }, [dispatch, alert, errores, isUpdated, navigate]);

  const SubmitHandler = (e) => {
    e.preventDefault();

    dispatch(updatePassword({ oldPassword, newPassword }));
  };

  return (
    <Fragment>
      <MetaData titulo={"Update Password"} />
      <div class="row wrapper">
        <div class="col-10 col-lg-5">
          <form class="shadow-lg" onSubmit={SubmitHandler}>
            <h1 class="mt-2 mb-5">Update Password</h1>
            <div class="form-group">
              <label for="old_password_field">Old Password</label>
              <input
                type="password"
                id="old_password_field"
                class="form-control"
                value={oldPassword}
                onChange={(e) => setOldPassword(e.target.value)}
              />
            </div>

            <div class="form-group">
              <label for="new_password_field">New Password</label>
              <input
                type="password"
                id="new_password_field"
                class="form-control"
                value={newPassword}
                onChange={(e) => setNewPassword(e.target.value)}
              />
            </div>

            <button
              type="submit"
              class="btn update-btn btn-block mt-4 mb-3"
              disabled={loading ? true : false}
            >
              Update Password
            </button>
          </form>
        </div>
      </div>
    </Fragment>
  );
};

export default UpdatePassword;

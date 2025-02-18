import React, { Fragment, useEffect, useState } from "react";
import MetaData from "../layout/MetaData";
import { useNavigate } from "react-router-dom";
import { useAlert } from "react-alert";
import { useDispatch, useSelector } from "react-redux";
import { resetUpdateStatus } from "../../slices/securitySlice";
import { Loader } from "../layout/Loader";
import { update } from "../../actions/userAction";

const UpdateProfile = () => {
  const navigate = useNavigate();
  const alert = useAlert();
  const dispatch = useDispatch();

  const [userSesion, setUserSession] = useState({
    nombre: "",
    apellido: "",
    telefono: "",
  });

  const [avatar, setAvatar] = useState("");
  const [avatarPreview, setAvatarPreview] = useState(
    "/images/default_avatar.jpg"
  );

  const { errors, isAuthenticated, loading, user, isUpdated } = useSelector(
    (state) => state.security
  );

  useEffect(() => {
    if (isAuthenticated) {
      setUserSession({ ...user });
      setAvatarPreview(user.avatar);
    } else {
      navigate("/login");
    }

    if (errors) {
      errors.map((error) => alert.error(error));
    }

    if (isUpdated) {
      alert.success("Se actualizó exitosamente el usuario");
      navigate("/me");
      dispatch(resetUpdateStatus({}));
    }
  }, [isAuthenticated, errors, alert, user, isUpdated, dispatch, navigate]);

  if (loading) {
    return <Loader />;
  }

  const onChange = (e) => {
    if (e.target.name === "avatar") {
      const reader = new FileReader();

      reader.onload = () => {
        if (reader.readyState === 2) {
          setAvatarPreview(reader.result);
          setAvatar(e.target.files[0]);
        }
      };

      reader.readAsDataURL(e.target.files[0]);
    } else {
      setUserSession({ ...userSesion, [e.target.name]: e.target.value });
    }
  };

  const submitHandler = (e) => {
    e.preventDefault();

    const formData = new FormData();
    formData.set("nombre", userSesion.nombre);
    formData.set("apellido", userSesion.apellido);
    formData.set("telefono", userSesion.telefono);
    formData.set("email", userSesion.email);
    formData.set("username", userSesion.username);
    formData.set("foto", avatar);

    dispatch(update(formData));
  };

  return (
    <Fragment>
      <MetaData titulo={"Update profile"} />
      <div className="row wrapper">
        <div className="col-10 col-lg-5">
          <form
            className="shadow-lg"
            encType="multipart/form-data"
            onSubmit={submitHandler}
          >
            <h1 className="mt-2 mb-5">Update Profile</h1>

            <div className="form-group">
              <label htmlFor="name_field">Name</label>
              <input
                type="text"
                id="name_field"
                className="form-control"
                name="nombre"
                value={userSesion.nombre}
                onChange={onChange}
              />
            </div>

            <div className="form-group">
              <label htmlFor="name_field">Lastname</label>
              <input
                type="text"
                id="lasname_field"
                className="form-control"
                name="apellido"
                value={userSesion.apellido}
                onChange={onChange}
              />
            </div>

            <div className="form-group">
              <label htmlFor="name_field">Phone</label>
              <input
                type="text"
                id="lasname_field"
                className="form-control"
                name="apellido"
                value={userSesion.telefono}
                onChange={onChange}
              />
            </div>

            <div className="form-group">
              <label htmlFor="avatar_upload">Avatar</label>
              <div className="d-flex align-items-center">
                <div>
                  <figure className="avatar mr-3 item-rtl">
                    <img
                      src={avatarPreview}
                      className="rounded-circle"
                      alt="Avatar Preview"
                    />
                  </figure>
                </div>
                <div className="custom-file">
                  <input
                    type="file"
                    name="avatar"
                    className="custom-file-input"
                    id="customFile"
                    accept="images/*"
                    onChange={onChange}
                  />
                  <label className="custom-file-label" htmlFor="customFile">
                    Choose Avatar
                  </label>
                </div>
              </div>
            </div>

            <button
              type="submit"
              className="btn update-btn btn-block mt-4 mb-3"
              disabled={loading ? true : false}
            >
              Update
            </button>
          </form>
        </div>
      </div>
    </Fragment>
  );
};

export default UpdateProfile;

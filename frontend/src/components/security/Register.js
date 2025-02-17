import React, { Fragment, useEffect, useState } from "react";
import { useAlert } from "react-alert";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import { register } from "../../actions/userAction";
import MetaData from "../layout/MetaData";
import { Loader } from "../layout/Loader";

const Register = () => {
  //const default_image = "./images/default_avatar.jpg";
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const alert = useAlert();

  const [user, setUser] = useState({
    nombre: "",
    apellido: "",
    email: "",
    password: "",
    telefono: "",
    username: "",
  });

  const { nombre, apellido, email, password, telefono, username } = user;
  const [avatar, setAvatar] = useState("");
  const [avatarPreview, setAvatarPreview] = useState(
    "./images/default_avatar.jpg"
  );

  //   const [name, setName] = useState();
  //   const [email, setEmail] = useState();
  //   const [password, setPassword] = useState();

  const { errors, loading, isAuthenticated } = useSelector(
    (state) => state.security
  );

  useEffect(() => {
    if (isAuthenticated) {
      navigate("/");
    }

    if (errors) {
      errors.map((error) => alert.error(error));
    }
  }, [dispatch, alert, isAuthenticated, errors, navigate]);

  const submitHandler = (e) => {
    e.preventDefault();

    const formData = new FormData();

    formData.set("nombre", nombre);
    formData.set("apellido", apellido);
    formData.set("telefono", telefono);
    formData.set("username", username);
    formData.set("email", email);
    formData.set("password", password);
    formData.set("foto", avatar);

    dispatch(register({ formData }));
  };

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
      setUser({ ...user, [e.target.name]: e.target.value });
    }
  };

  return (
    <Fragment>
      <MetaData titulo={"Register"} />
      <div className="row wrapper">
        <div className="col-10 col-lg-5">
          <form
            className="shadow-lg"
            encType="multipart/form-data"
            onSubmit={submitHandler}
          >
            <h1 className="mb-3">Register</h1>

            <div className="form-group">
              <label htmlFor="name_field">Name</label>
              <input
                type="text"
                id="name_field"
                className="form-control"
                value={nombre}
                name="name"
                onChange={onChange}
              />
            </div>

            <div className="form-group">
              <label htmlFor="lastname_field">Lastname</label>
              <input
                type="text"
                id="lastname_field"
                className="form-control"
                value={apellido}
                name="lastname"
                onChange={onChange}
              />
            </div>

            <div className="form-group">
              <label htmlFor="phone_field">Phone</label>
              <input
                type="text"
                id="phone_field"
                className="form-control"
                value={telefono}
                name="phone"
                onChange={onChange}
              />
            </div>

            <div className="form-group">
              <label htmlFor="username_field">Username</label>
              <input
                type="text"
                id="username_field"
                className="form-control"
                value={username}
                name="username"
                onChange={onChange}
              />
            </div>

            <div className="form-group">
              <label htmlFor="email_field">Email</label>
              <input
                type="email"
                id="email_field"
                className="form-control"
                value={email}
                name="email"
                onChange={onChange}
              />
            </div>

            <div className="form-group">
              <label htmlFor="password_field">Password</label>
              <input
                type="password"
                id="password_field"
                className="form-control"
                value={password}
                name="password"
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
                      alt="previewImg-register"
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
                    Upload Avatar
                  </label>
                </div>
              </div>
            </div>

            <button
              id="register_button"
              type="submit"
              className="btn btn-block py-3"
            >
              REGISTER
            </button>
          </form>
        </div>
      </div>
    </Fragment>
  );
};

export default Register;

import React, { Fragment } from "react";
import MetaData from "../layout/MetaData";
import { useSelector } from "react-redux";
import { Link, useNavigate } from "react-router-dom";
import { Loader } from "../layout/Loader";
import { useAlert } from "react-alert";

const Profile = () => {
  const { user, loading, isAuthenticated, errors } = useSelector(
    (state) => state.security
  );

  const navigate = useNavigate();
  const alert = useAlert();

  if (loading) {
    return <Loader />;
  }

  if (!isAuthenticated) {
    navigate("/login");
  }

  if (errors) {
    errors.map((error) => alert.error(error));
  }

  return (
    <Fragment>
      <MetaData titulo={"Profile"} />
      <h2 className="mt-5 ml-5">My Profile</h2>
      <div className="row justify-content-around mt-5 user-info">
        <div className="col-12 col-md-3">
          <figure className="avatar avatar-profile">
            <img
              className="rounded-circle img-fluid"
              src={user && user.avatar}
              alt={user && user.nombre}
            />
          </figure>
          <Link
            to="/me/update"
            id="edit_profile"
            className="btn btn-primary btn-block my-5"
          >
            Edit Profile
          </Link>
        </div>

        <div className="col-12 col-md-5">
          <h4>Name</h4>
          <p>{user && user.nombre}</p>

          <h4>Lastname</h4>
          <p>{user && user.apellido}</p>

          <h4>Phone</h4>
          <p>{user && user.telefono}</p>

          <h4>Username</h4>
          <p>{user && user.username}</p>

          <h4>Email</h4>
          <p>{user && user.email}</p>

          {user && !user.roles.includes("ADMIN") && (
            <Link to="/orders/me" className="btn btn-danger btn-block mt-5">
              My Orders
            </Link>
          )}

          <Link to="password/update" className="btn btn-primary btn-block mt-3">
            Change Password
          </Link>
        </div>
      </div>
    </Fragment>
  );
};

export default Profile;

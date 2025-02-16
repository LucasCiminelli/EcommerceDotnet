import React, { Fragment } from "react";
import "../../App.css";
import Search from "./Search";
import { Link } from "react-router-dom";

export const Header = () => {
  return (
    <Fragment>
      <nav className="navbar row">
        <div className="col-12 col-md-3">
          <div className="navbar-brand">
            <img src="/images/logo_vaxi.png" alt="logo" />
          </div>
        </div>

        <div className="col-12 col-md-6 mt-2 mt-md-0">
          <Search />
        </div>

        <div className="col-12 col-md-3 mt-4 mt-md-0 text-center">
          <Link
            className="btn"
            id="login_btn"
            style={{ textDecoration: "none" }}
            to={"/login"}
          >
            Login
          </Link>

          <span id="cart" className="ml-3">
            Cart
          </span>
          <span className="ml-1" id="cart_count">
            2
          </span>
        </div>
      </nav>
    </Fragment>
  );
};

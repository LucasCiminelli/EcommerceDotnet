import React, { useEffect, useState } from "react";
import { Fragment } from "react";
import MetaData from "../layout/MetaData";
import { useAlert } from "react-alert";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import { resetUpdateStatus } from "../../slices/securitySlice";
import { saveAddressInfo } from "../../actions/cartAction";

const Shipping = () => {
  const alert = useAlert();
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const { user, errores, isUpdated } = useSelector((state) => state.security);

  const { countries } = useSelector((state) => state.countries);

  const [direccion, setDireccion] = useState(
    user.direccionEnvio ? user.direccionEnvio.direccion : ""
  );

  const [ciudad, setCiudad] = useState(
    user.direccionEnvio ? user.direccionEnvio.ciudad : ""
  );

  const [departamento, setDepartamento] = useState(
    user.direccionEnvio ? user.direccionEnvio.departamento : ""
  );

  const [codigoPostal, setCodigoPostal] = useState(
    user.direccionEnvio ? user.direccionEnvio.codigoPostal : ""
  );

  const [pais, setPais] = useState(
    user.direccionEnvio ? user.direccionEnvio.pais : ""
  );

  useEffect(() => {
    if (isUpdated) {
      // navigate("order/payment");
      dispatch(resetUpdateStatus());
      alert.success("se almacenó la dirección de envio");
    }

    if (errores) {
      errores.map((error) => alert.error(error));
    }
  }, [dispatch, errores, alert, isUpdated]);

  const submitHandler = (e) => {
    e.preventDefault();

    const request = {
      direccion,
      ciudad,
      departamento,
      codigoPostal,
      pais,
    };

    dispatch(saveAddressInfo(request));
  };

  return (
    <Fragment>
      <MetaData titulo={"Shipping"} />
      <div className="row wrapper">
        <div className="col-10 col-lg-5">
          <form className="shadow-lg" onSubmit={submitHandler}>
            <h1 className="mb-4">Shipping Info</h1>
            <div className="form-group">
              <label htmlFor="address_field">Address</label>
              <input
                type="text"
                id="address_field"
                className="form-control"
                value={direccion ?? ""}
                required
                onChange={(e) => setDireccion(e.target.value)}
              />
            </div>

            <div className="form-group">
              <label htmlFor="city_field">City</label>
              <input
                type="text"
                id="city_field"
                className="form-control"
                value={ciudad ?? ""}
                required
                onChange={(e) => setCiudad(e.target.value)}
              />
            </div>

            <div className="form-group">
              <label htmlFor="department_field">Department</label>
              <input
                type="text"
                id="department_field"
                className="form-control"
                value={departamento ?? ""}
                required
                onChange={(e) => setDepartamento(e.target.value)}
              />
            </div>

            <div className="form-group">
              <label htmlFor="postal_code_field">Postal Code</label>
              <input
                type="number"
                id="postal_code_field"
                className="form-control"
                value={codigoPostal ?? ""}
                required
                onChange={(e) => setCodigoPostal(e.target.value)}
              />
            </div>

            <div className="form-group">
              <label htmlFor="country_field">Country</label>
              <select
                id="country_field"
                className="form-control"
                value={pais ?? ""}
                required
                onChange={(e) => setPais(e.target.value)}
              >
                {countries.map((country) => (
                  <option key={country.id} value={country.name}>
                    {country.name}
                  </option>
                ))}
              </select>
            </div>

            <button
              id="shipping_btn"
              type="submit"
              className="btn btn-block py-3"
            >
              CONTINUE
            </button>
          </form>
        </div>
      </div>
    </Fragment>
  );
};

export default Shipping;

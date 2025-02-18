import { Outlet, Navigate } from "react-router-dom";
import { parseToken } from "../../utilities/parseToken";

const ProtectedRoute = ({ existsRoles }) => {
  const token = localStorage.getItem("token");

  if (!token) {
    return <Navigate to="/login" />;
  }

  if (!existsRoles) {
    return <Outlet />;
  }

  const payload = parseToken(token);

  console.log(payload);

  const { role } = payload;

  if (Array.isArray(role)) {
    // En caso de que role sea un array (no parece ser el caso con tu token actual)
    const isFounded = role.some((r) => existsRoles.includes(r));
    return isFounded ? <Outlet /> : <Navigate to="/login" />;
  } else {
    // Cuando role es un string (caso real en tu token)
    return existsRoles.includes(role) ? <Outlet /> : <Navigate to="/login" />;
  }
};

export default ProtectedRoute;

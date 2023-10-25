import { React } from "react";
import useUser from "../hooks/useUser";
import loadingStatus from "../helpers/loadingStatus";

const Login = () => {
  const { isAuthenticated, login, getNameClaim, loadingState } = useUser();

  if (loadingState === loadingStatus.isLoading) {
    return <h4>Loading..</h4>;
  }

  if (isAuthenticated) {
    var user = getNameClaim();
    return <p>Hi {user}!</p>;
  } else {
    return (
      <button onClick={login} className="btn btn-primary">
        Login
      </button>
    );
  }
};

export default Login;

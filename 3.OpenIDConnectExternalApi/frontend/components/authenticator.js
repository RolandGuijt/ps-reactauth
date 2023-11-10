import { React } from "react";
import useUser from "../hooks/useUser";
import loadingStatus from "../helpers/loadingStatus";

const Authenticator = () => {
  const { isAuthenticated, login, logout, getNameClaim, loadingState } =
    useUser();

  if (loadingState === loadingStatus.isLoading) {
    return <h4>Loading..</h4>;
  }

  if (isAuthenticated) {
    var userName = getNameClaim();
    return (
      <div>
        Hi {userName}!
        <div>
          <button onClick={logout} className="mt-3 btn btn-secondary btn-sm">
            Logout
          </button>
        </div>
      </div>
    );
  } else {
    return (
      <button onClick={login} className="btn btn-primary">
        Login
      </button>
    );
  }
};

export default Authenticator;

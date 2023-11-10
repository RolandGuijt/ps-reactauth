import useGetRequest from "./useGetRequest";
import { useCallback, useEffect, useState } from "react";

const useUser = () => {
  const { get, loadingState } = useGetRequest("/account/user?slide=false");
  const [claims, setClaims] = useState([]);
  const [isAuthenticated, setIsAuthenticated] = useState(false);

  useEffect(() => {
    const getClaims = async () => {
      const claims = await get();
      setClaims(claims);
      claims && claims.length > 0 && setIsAuthenticated(true);
    };
    getClaims();
  }, [get]);

  const login = useCallback(
    () => window.location.replace("/account/login"),
    []
  );
  const logout = useCallback(() => {
    let logoutUrl = claims?.find(
      (claim) => claim.type === "bff:logout_url"
    ).value;
    window.location.replace(logoutUrl);
  }, [claims]);

  const getNameClaim = useCallback(
    () => claims?.find((claim) => claim.type === "name").value,
    [claims]
  );

  return {
    claims,
    loadingState,
    isAuthenticated,
    getNameClaim,
    login,
    logout,
  };
};

export default useUser;

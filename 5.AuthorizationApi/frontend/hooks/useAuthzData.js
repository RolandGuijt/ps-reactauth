import { useCallback, useEffect, useState } from "react";
import useGetRequest from "./useGetRequest";

const useAuthzData = () => {
  const [authzData, setAuthzData] = useState([]);
  const { get, loadingState } = useGetRequest("/auth/user/settings");

  useEffect(() => {
    const getAuthzData = async () => {
      const data = await get();
      setAuthzData(data);
    };
    getAuthzData();
  }, [get]);

  const getBidsEnabled = useCallback(() => {
    return authzData.displayBids === "true";
  });

  return {
    loadingState,
    getBidsEnabled,
  };
};

export default useAuthzData;

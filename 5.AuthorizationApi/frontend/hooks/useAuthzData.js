import { useEffect, useState } from "react";
import useGetRequest from "./useGetRequest";

const useAuthzData = () => {
  const [authzData, setAuthzData] = useState([]);
  const { get, loadingState } = useGetRequest("/auth/user/authzdata/1");

  useEffect(() => {
    const getAuthzData = async () => {
      const data = await get();
      setAuthzData(data);
    };
    getAuthzData();
  }, [get]);

  return {
    loadingState,
    authzData,
  };
};

export default useAuthzData;

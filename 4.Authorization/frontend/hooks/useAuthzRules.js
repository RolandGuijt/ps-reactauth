import { useCallback } from "react";
import useAuthzData from "./useAuthzData";
import useUser from "./useUser";

const useAuthzRules = () => {
  const { claims } = useUser();
  const { authzData } = useAuthzData();

  const canEnterNewBid = useCallback(() => {
    let claim = claims?.find((claim) => claim.type === "role");
    return claim ? claim.value === "Admin" : false;
  });

  const canDisplayBids = useCallback(() => {
    let claim = claims?.find((claim) => claim.type === "role");
    let isAdmin = claim ? claim.value === "Admin" : false;
    let displayBids = authzData?.find((d) => d.type === "displayBids");
    let canDisplayBids = displayBids ? displayBids.value === "true" : false;

    return isAdmin && canDisplayBids;
  });

  return {
    canEnterNewBid,
    canDisplayBids,
  };
};

export default useAuthzRules;

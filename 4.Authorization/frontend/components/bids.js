import { useState } from "react";
import currencyFormatter from "../helpers/currencyFormatter";
import loadingStatus from "../helpers/loadingStatus";
import useBids from "../hooks/useBids";
import LoadingIndicator from "./loadingIndicator";
import AccessDenied from "./accessDenied";
import useAuthzData from "../hooks/useAuthzData";
import useUser from "../hooks/useUser";

const Bids = ({ house }) => {
  const { getIsAdmin } = useUser();
  const { bids, loadingState, addBid } = useBids(house.id);
  const { getBidsEnabled, authzLoadingState } = useAuthzData();

  const emptyBid = {
    houseId: house.id,
    bidder: "",
    amount: 0,
  };

  const [newBid, setNewBid] = useState(emptyBid);

  if (
    loadingState !== loadingStatus.loaded ||
    authzLoadingState != loadingState.loaded
  )
    return <LoadingIndicator loadingState={loadingState} />;

  if (!getIsAdmin() || !getBidsEnabled()) return <AccessDenied />;

  const onBidSubmitClick = () => {
    addBid(newBid);
    setNewBid(emptyBid);
  };

  return (
    <>
      <div className="row mt-4">
        <div className="col-12">
          <table className="table table-sm">
            <thead>
              <tr>
                <th>Bidder</th>
                <th>Amount</th>
              </tr>
            </thead>
            <tbody>
              {bids.map((b) => (
                <tr key={b.id}>
                  <td>{b.bidderName}</td>
                  <td>{currencyFormatter.format(b.amount)}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
      {getIsAdmin() && (
        <div className="row">
          <div className="col-4">
            <input
              id="bidder"
              className="h-100"
              type="text"
              value={newBid.bidder}
              onChange={(e) => setNewBid({ ...newBid, bidder: e.target.value })}
              placeholder="Bidder"
            ></input>
          </div>
          <div className="col-4">
            <input
              id="amount"
              className="h-100"
              type="number"
              value={newBid.amount}
              onChange={(e) =>
                setNewBid({ ...newBid, amount: parseInt(e.target.value) })
              }
              placeholder="Amount"
            ></input>
          </div>
          <div className="col-2">
            <button className="btn btn-primary" onClick={onBidSubmitClick}>
              Add
            </button>
          </div>
        </div>
      )}
    </>
  );
};

export default Bids;

import useHouses from "../hooks/useHouses";
import loadingStates from "../helpers/loadingStatus";
import LoadingIndicator from "./loadingIndicator";
import usePostHouse from "../hooks/usePostHouse";
import HouseRow from "./houseRow";

const HouseList = () => {
  const { houses, setHouses, loadingState } = useHouses();
  const { post } = usePostHouse();

  if (loadingState !== loadingStates.loaded)
    return <LoadingIndicator loadingState={loadingState} />;

  const addHouse = () => {
    const newHouse = {
      id: 3,
      address: "32 Valley Way, New York",
      country: "USA",
      price: 1000000,
    };
    post(newHouse);
    setHouses([...houses, newHouse]);
  };

  return (
    <div>
      <div className="row mb-2">
        <h5 className="themeFontColor text-center">
          Houses currently on the market
        </h5>
      </div>
      <table className="table table-hover">
        <thead>
          <tr>
            <th>Address</th>
            <th>Country</th>
            <th>Asking Price</th>
          </tr>
        </thead>
        <tbody>
          {houses.map((house) => (
            <HouseRow key={house.id} house={house} />
          ))}
        </tbody>
      </table>
      <button className="btn btn-primary" onClick={addHouse}>
        Add
      </button>
    </div>
  );
};

export default HouseList;

import React, { useContext } from "react";
import currencyFormatter from "../helpers/currencyFormatter";
import navValues from "../helpers/navValues";
import { NavigationContext } from "./app";

const HouseRow = ({ house }) => {
  const { navigate } = useContext(NavigationContext);
  return (
    <tr onClick={() => navigate(navValues.house, house)}>
      <td>{house.address}</td>
      <td>{house.country}</td>
      {house.price && (
        <td className={`${house.price >= 500000 ? "text-primary" : ""}`}>
          {currencyFormatter.format(house.price)}
        </td>
      )}
    </tr>
  );
};

export const HouseRowMem = React.memo(HouseRow);
export default HouseRow;

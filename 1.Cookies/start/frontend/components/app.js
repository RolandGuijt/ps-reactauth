import Banner from "./banner";
import navValues from "../helpers/navValues";
import React, { useCallback, useState } from "react";
import ComponentPicker from "./componentPicker";
import Login from "./login";

const NavigationContext = React.createContext({
  navState: { current: navValues.home },
});

const App = () => {
  const navigate = useCallback(
    (navTo, param) => setNav({ current: navTo, param, navigate }),
    []
  );

  const [nav, setNav] = useState({
    current: navValues.home,
    navigate,
  });

  return (
    <NavigationContext.Provider value={nav}>
      <Banner>Providing houses all over the world</Banner>
      <Login />
      <ComponentPicker currentNavLocation={nav.current} />
    </NavigationContext.Provider>
  );
};

export { NavigationContext };
export default App;
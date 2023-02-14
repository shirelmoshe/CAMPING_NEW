import { Main } from "./components/main/main";
import { LoginButton } from "./components/auth0/logIn.button";
import { useAuth0 } from "@auth0/auth0-react";
//import { Route, Routes } from "react-router-dom";

//import { Home } from "./components/pages/Home/Home";
//import { TwitterTable } from "./components/pages/TwitterTable/TwitterTable";

export const App = () => {
  const { isAuthenticated, isLoading } = useAuth0();

  if (!isLoading) {
    return (
      <div className="App">{isAuthenticated ? <Main /> : <LoginButton />}</div>
    );
  } else {
    return "Loading";
  }
};

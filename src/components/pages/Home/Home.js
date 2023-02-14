import React from "react";
import { Button } from "../../Button/Button";
//import { Navbar } from "../../Navbar/Navbar";
import "./Home.css";
export const Home = () => {
  return (
    <>
      <div className="Home">
        <h2>ProLobby</h2>

        <div className="hero-btns">
          <Button
            className="btns"
            buttonStyle="btn--outline"
            buttonSize="btn--large"
          >
            sing in
          </Button>
        </div>
      </div>
    </>
  );
};

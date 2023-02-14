import React from "react";
import { Routes, Route } from "react-router-dom";
import { Home } from "../pages/Home/Home";
import { Shipping } from "../pages/Shipping/Shipping";
import { CompanyOwnerUser } from "./../pages/companyOwnerUser/companyOwnerUser";
import { Donation } from "./../pages/Donation/Donation";
import { Campaigns } from "./../pages/campaigns/campaigns";
import { Products } from "./../pages/Products/products";

import { SingUp } from "./../pages/SignUp/SignUp";
import { NavbarCompanyOwner } from "../NavbarNavbarCompanyOwner/NavbarNavbarCompanyOwner";
import { useEffect } from "react";
import { useState } from "react";
import { GetRoles } from "../servers/servers";
import { useAuth0 } from "@auth0/auth0-react";

export const CompanyOwner = () => {
  const { user } = useAuth0();
  const [role, setRole] = useState([]);

  let user_id = user.sub;
  const handleRoles = async () => {
    let roles = await GetRoles(user_id);

    setRole(roles);
  };
  useEffect(() => {
    handleRoles();
  }, []);
  return (
    <>
      <NavbarCompanyOwner />
      <Routes>
        <Route path="/" element={<Home />}></Route>
        <Route path="/donation" element={<Donation />}></Route>
        <Route
          path="/CompanyOwnerUser"
          element={<CompanyOwnerUser user={user_id} />}
        ></Route>
        <Route path="/Campaigns" element={<Campaigns />}></Route>
        <Route path="/SnigUp" element={<SingUp />}></Route>
        <Route path="/Products" element={<Products />}></Route>
        <Route path="/Shipping" element={<Shipping user={user_id} />}></Route>
      </Routes>
    </>
  );
};

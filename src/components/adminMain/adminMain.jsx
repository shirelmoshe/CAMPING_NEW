import { Home } from "../pages/Home/Home";
import { Routes, Route } from "react-router-dom";
import { UsersTable } from "../pages/Users/user";
import { TwitterTable } from "../pages/TwitterTable/TwitterTable";
import { AdminUser } from "../AdminUser/Admin";
import { CampaingsTable } from "../pages/CampaingsTable/CampaingsTable";
import { SalesTable } from "../pages/Sales/sales";
import { Campaigns } from "./../pages/campaigns/campaigns";
import { SingUp } from "./../pages/SignUp/SignUp";
import { Products } from "./../pages/Products/products";
import { ProductData } from "../productData/ProductInfo";
import React from "react";
import NavbarAdmin from "../NavabarAdmin/NavabarAdmin";
import { useEffect } from "react";
import { useState } from "react";
import { GetRoles } from "../servers/servers";
import { useAuth0 } from "@auth0/auth0-react";

import { ThemeContext } from "@emotion/react";
import { UpdateUser } from "../update-user/update-user";

export const Admin = () => {
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

  const [product, setProduct] = useState({});

  return (
    <>
      <ThemeContext.Provider value={{ product, setProduct }}>
        <NavbarAdmin />
        <Routes>
          <Route path="/" element={<Home />}></Route>
          <Route path="/Campaigns" element={<Campaigns />}></Route>
          <Route path="/SnigUp" element={<SingUp />}></Route>
          <Route path="/Products" element={<Products />}></Route>ז
          <Route path="/Sales" element={<SalesTable />}></Route>
          <Route
            path={`/Products${product.productId}`}
            element={
              <ProductData
                CampaignName={product.CampaignName}
                Product={product.Product}
                Price={product.Price}
                productId={product.productId}
              />
            }
          />
          <Route path="/User" element={<UsersTable />}></Route>
          <Route path="/TwitterTable" element={<TwitterTable />}></Route>
          <Route path="/CampaingsTable" element={<CampaingsTable />}></Route>
          <Route
            path="/getadmin"
            element={<AdminUser user={user_id} />}
          ></Route>
          <Route
            path={`/update-user${user_id}`}
            element={<UpdateUser user={user_id} />}
          ></Route>
        </Routes>
      </ThemeContext.Provider>
    </>
  );
};

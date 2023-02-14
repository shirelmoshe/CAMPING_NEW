import React, { useState, useEffect } from "react";
import { useAuth0 } from "@auth0/auth0-react";
import { GetRoles } from "./../servers/servers";
import { Home } from "../pages/Home/Home";
import { LogOutButton } from "../auth0/logOut.button";
import { Admin } from "../adminMain/adminMain";
import { CompanyOwner } from "../CompanyOwnerMain/CompanyOwnerMain";
import { SocialActivists } from "../socialActivistMain/socialActivistMain";
import { AssociationRepresentative } from "../AssociationRepresentativeMain/AssociationRepresentativeMain";
//import { NavbarSocialActivist } from "./../Navbar/Navbar";

export const Main = () => {
  const { user } = useAuth0();
  const [role, setRole] = useState([]);

  let userId = user.sub;
  const handleRoles = async () => {
    let roles = await GetRoles(userId);

    setRole(roles);
  };
  useEffect(() => {
    handleRoles();
  }, []);

  return (
    <>
      <LogOutButton />

      {role.length > 0 ? (
        role.map((userrole) => {
          if (userrole.name === "Association representative")
            return <AssociationRepresentative />;
          else if (userrole.name === "social activist")
            return <SocialActivists />;
          else if (userrole.name === "Company owner") return <CompanyOwner />;
          else if (userrole.name === "Admin") return <Admin />;
          else return <Home />;
        })
      ) : (
        <>error</>
      )}
    </>
  );
};

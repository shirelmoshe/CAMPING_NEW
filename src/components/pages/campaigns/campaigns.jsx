import { getCampaigns } from "../../servers/servers";
import React from "react";
import { useState } from "react";
import { useEffect } from "react";
import { DimaniCard } from "../../DimaniCard/DimaniCard.jsx";

export const Campaigns = () => {
  const [productsInfo, setProductsInfo] = useState([]);
  const initProductsInfo = async () => {
    let response = await getCampaigns();
    if (response && typeof response === "object") {
      let CampaignsArr = Object.values(response);
      setProductsInfo(CampaignsArr);
    } else {
      console.log("error");
    }
  };

  useEffect(() => {
    initProductsInfo();
  }, []);

  return (
    <>
      {productsInfo &&
        productsInfo.map((response) => {
          const { associationName, email, uri, usreId, hashtag } = response;
          return (
            <DimaniCard
              key={usreId}
              associationName={associationName}
              email={email}
              uri={uri}
              usreId={usreId}
              hashtag={hashtag}
            ></DimaniCard>
          );
        })}
    </>
  );
};

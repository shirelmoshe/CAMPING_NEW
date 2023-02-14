import { getCampaigns } from "../../servers/servers";
import React from "react";
import { useState } from "react";
import { useEffect } from "react";
import { CampaignTableIDCard } from "../CampaignTableIDCard/CampaignTableIDCard";

export const ChangeCampaign = ({ user }) => {
  const [CampaingsTable, setCampaingsTable] = useState([]);
  const initProductsInfo = async () => {
    let response = await getCampaigns(user);
    if (response && typeof response === "object") {
      let CampaignsArr = Object.values(response);
      setCampaingsTable(CampaignsArr);
    } else {
      console.log("error");
    }
  };

  useEffect(() => {
    initProductsInfo();
  }, []);

  return (
    <>
      <table className="table table-striped">
        <thead className="table table-striped">
          <tr>
            <th scope="col">associationName</th>
            <th scope="col">email</th>
            <th scope="col">uri</th>
            <th scope="col">hashtag</th>
            <th scope="col">CampaignName</th>
          </tr>
        </thead>
        <tbody className="table table-striped">
          {CampaingsTable &&
            CampaingsTable.map((response) => {
              const {
                userId,
                associationName,
                email,
                uri,
                hashtag,
                CampaignName,
              } = response;
              return (
                <CampaignTableIDCard
                  key={userId}
                  associationName={associationName}
                  email={email}
                  uri={uri}
                  hashtag={hashtag}
                  CampaignName={CampaignName}
                ></CampaignTableIDCard>
              );
            })}
        </tbody>
      </table>
    </>
  );
};

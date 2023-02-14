import React, { useState, useEffect } from "react";
import { getCampaingsTable } from "../../servers/servers";
//import { useContext } from "react";
import { CardCampings } from "../../CampaingsTableCard/CampaingsTableCard";
//import { CampaingContext } from "../context/context";

export const CampaingsTable = () => {
  const [CampaingsTable, setCampaingsTable] = useState([]);
  const [filteredTable, setFilteredTable] = useState([]);
  const [searchTerm, setSearchTerm] = useState("");
  // const { setCampaign } = useContext(CampaingContext);
  const initSalesInfo = async () => {
    let response = await getCampaingsTable();
    if (response && typeof response === "object") {
      let CampaignsArr = Object.values(response);
      setCampaingsTable(CampaignsArr);
      setFilteredTable(CampaignsArr);
    } else {
      console.log("error");
    }
  };

  useEffect(() => {
    initSalesInfo();
  }, []);

  useEffect(() => {
    setFilteredTable(
      CampaingsTable.filter((campaign) =>
        campaign.associationName
          .toLowerCase()
          .includes(searchTerm.toLowerCase().trim())
      )
    );
  }, [searchTerm, CampaingsTable]);

  return (
    <>
      <input
        type="text"
        placeholder="Search"
        value={searchTerm}
        onChange={(e) => setSearchTerm(e.target.value)}
      />
      <table class="table table-hover">
        <thead class="table table-hover">
          <tr>
            <th scope="col">associationName</th>
            <th scope="col">email</th>
            <th scope="col">uri</th>
            <th scope="col">hashtag</th>
            <th scope="col">CampaignName</th>
          </tr>
        </thead>
        <tbody class="table table-hover">
          {filteredTable &&
            filteredTable.map((response) => {
              const {
                userId,
                associationName,
                email,
                uri,
                hashtag,
                CampaignName,
              } = response;
              return (
                <CardCampings
                  key={userId}
                  associationName={associationName}
                  email={email}
                  uri={uri}
                  hashtag={hashtag}
                  CampaignName={CampaignName}
                ></CardCampings>
              );
            })}
        </tbody>
      </table>
    </>
  );
};

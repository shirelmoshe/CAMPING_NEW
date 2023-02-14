import React, { useState, useEffect } from "react";
import { CampaignTableByID } from "../../servers/servers";
import { CampaignTableIDCard } from "../CampaignTableIDCard/CampaignTableIDCard";
import { CampaignUpdateContext } from "../../context/campaignUpdateContext";
import { useContext } from "react";

export const CampaignTableID = ({ user }) => {
  const [campaignTableID, setCampaignTableID] = useState([]);
  const [search, setSearch] = useState(""); // הוספת משתנה חיפוש
  const { setCampaign } = useContext(CampaignUpdateContext);
  const initTwitterTableInfo = async () => {
    try {
      console.log(user);

      let response = await CampaignTableByID(user);

      if (response && typeof response === "object") {
        let CampaignsArr = Object.values(response);
        setCampaignTableID(CampaignsArr);
      }
    } catch (error) {
      console.log("error");
    }
  };
  useEffect(() => {
    initTwitterTableInfo();
  }, []);

  const filteredTable = campaignTableID.filter(
    (item) =>
      item.associationName.toLowerCase().includes(search.toLowerCase()) ||
      item.email.toLowerCase().includes(search.toLowerCase()) ||
      item.uri.toLowerCase().includes(search.toLowerCase()) ||
      item.hashtag.toLowerCase().includes(search.toLowerCase()) ||
      item.CampaignName.toLowerCase().includes(search.toLowerCase())
  );
  const CampingUrl = (objectProduct) => {
    console.log("aa", objectProduct);
    setCampaign(objectProduct);
  };

  return (
    <>
      <input
        type="text"
        placeholder="Search"
        value={search}
        onChange={(e) => setSearch(e.target.value)}
      />
      <table class="table table-striped">
        <thead className="table table-striped">
          <tr>
            <th scope="col">associationName</th>
            <th scope="col">email</th>
            <th scope="col">uri</th>
            <th scope="col">hashtag</th>
            <th scope="col">CampaignName</th>
            <th scope="col">Update</th>
            <th scope="col">deletion</th>
          </tr>
        </thead>
        <tbody class="table table-striped">
          {filteredTable &&
            filteredTable.map((p) => {
              return (
                <CampaignTableIDCard
                  btnLink={CampingUrl}
                  campingObject={p}
                  changeSetfunction={setCampaign}
                ></CampaignTableIDCard>
              );
            })}
        </tbody>
      </table>
    </>
  );
};

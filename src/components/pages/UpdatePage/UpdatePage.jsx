import React, { useState, useContext } from "react";
import { Link } from "react-router-dom";
import { UpdateCampingAsync } from "../../servers/servers.js";
import { CampaignUpdateContext } from "../../context/campaignUpdateContext.jsx";

export const UpdatePage = () => {
  const { campaign } = useContext(CampaignUpdateContext);
  const [associationName, setAssociationName] = useState(
    campaign.associationName
  );
  const [email, setEmail] = useState(campaign.email);
  const [uri, setUri] = useState(campaign.uri);
  const [hashtag, setHashtag] = useState(campaign.hashtag);
  const [CampaignName, setCampaignName] = useState(campaign.CampaignName);

  const handleUpdateChanges = async () => {
    let upDateCamping = {
      userId: campaign.userId,

      associationName: associationName,
      email: email,
      uri: uri,
      hashtag: hashtag,
      CampaignName: CampaignName,
    };

    await UpdateCampingAsync(upDateCamping);
    return <Link to="/">back to the list</Link>;
  };

  return (
    <div className="form-group">
      <div className="col-md-4">
        <label htmlFor="validationCustom01" className="form-label">
          Association Name
        </label>
        <input
          aria-label={associationName}
          type="text"
          className="form-control"
          id="validationCustom01"
          value={associationName}
          onChange={(e) => setAssociationName(e.target.value)}
        />
        <div className="valid-feedback">Looks good!</div>
      </div>
      <div className="col-md-4">
        <label htmlFor="validationCustom01" className="form-label">
          Email
        </label>
        <input
          aria-label={email}
          type="text"
          className="form-control"
          id="validationCustom01"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
        <div className="valid-feedback">Looks good!</div>
      </div>
      <div className="col-md-4">
        <label htmlFor="validationCustom02" className="form-label">
          URI
        </label>
        <input
          aria-label={uri}
          type="text"
          className="form-control"
          id="validationCustom02"
          value={uri}
          onChange={(e) => setUri(e.target.value)}
        />
      </div>
      <div className="col-md-4">
        <label htmlFor="validationCustom02" className="form-label">
          Hashtag
        </label>
        <input
          aria-label={hashtag}
          type="text"
          className="form-control"
          id="validationCustom02"
          value={hashtag}
          onChange={(e) => setHashtag(e.target.value)}
        />
        <div className="col-md-4">
          <label for="validationCustom02" className="form-label">
            CampaignName
          </label>
          <input
            aria-label={CampaignName}
            type="text"
            className="form-control"
            id="validationCustom02"
            onChange={(e) => setCampaignName(e.target.value)}
          />
        </div>
        <button className="btn btn-primary" onClick={handleUpdateChanges}>
          Submit form
        </button>
      </div>
    </div>
  );
};

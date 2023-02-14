import React from "react";

export const CardCampings = ({
  usreId,
  associationName,
  email,
  uri,
  hashtag,
  CampaignName,
}) => {
  return (
    <>
      <tr>
        <td>{usreId}</td>
        <td>{associationName}</td>
        <td>{email}</td>
        <td>{uri}</td>
        <td>{hashtag}</td>
        <td>{CampaignName}</td>
      </tr>
    </>
  );
};

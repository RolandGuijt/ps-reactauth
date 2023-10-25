const usePostHouse = () => {
  return {
    post: (house) => {
      const postHouse = async () => {
        await fetch(`/houses`, {
          method: "POST",
          headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
          },
          body: JSON.stringify(house),
        });
      };
      postHouse();
    },
  };
};

export default usePostHouse;

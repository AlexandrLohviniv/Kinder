const usersLoaded = (listNewUsers) => {
    return {
        type: 'USERS_LOADED',
        payload: listNewUsers
    };
};

export {
    usersLoaded
};
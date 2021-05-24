const usersLoaded = (listNewUsers) => {
    return {
        type: 'USERS_LOADED',
        payload: listNewUsers
    };
};

const usersRequested = () => {
    return {
        type: 'USERS_REQUESTED',
    };
};

export {
    usersLoaded,
    usersRequested
};
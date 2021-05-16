import React from 'react';

import KinderServiceContext from '../KinderServiceContext';

const WithKinderService = () => (Wrapped) => {
    return (props) => {
        return (
            <KinderServiceContext.Consumer>
                {
                    (KinderService) => {
                        return <Wrapped {...props} KinderService={KinderService}/>
                    }
                }
            </KinderServiceContext.Consumer>
        )
    }
};

export default WithKinderService;
import React from 'react';
import DropdownRightButton from '../Dropdown-right-button';
import {Button} from 'react-bootstrap';


const MyTableItem = ({user, isBannedPage}) => {

    const whatItemToUse = () => {
        if(isBannedPage === "false") {
            return (
                <DropdownRightButton title={role} direction='right'/>
            )
        } else {
            return (
                <>
                    <Button variant="primary" size="sm">
                        Unban
                    </Button>
                    <Button variant="secondary" size="sm" disabled>
                        Ban
                    </Button>
                </>
            )
        }
    }

    const {id, firstName, lastName, role, nickName, email} = user;
    return(
        
            <tr>
                <td>{id}</td>
                <td>{firstName}</td>
                <td>{lastName}</td>
                <td>{nickName}</td>
                <td>{email}</td>
                <td>
                    {
                        whatItemToUse()
                    }
                    {/* <DropdownRightButton title={role} direction='right'/> */}
                </td>
            </tr>
    )
}

export default MyTableItem;
// export default class MyTableItem extends Component {
//     render() {
//         const {id, firstName, lastName, role, nickName, email} = this.props;

//         return (
//             <tr>
//                 <td>{id}</td>
//                 <td>{firstName}</td>
//                 <td>{lastName}</td>
//                 <td>{nickName}</td>
//                 <td>{email}</td>
//                 <td>{role}</td>
//             </tr>
//         )
//     }
// }


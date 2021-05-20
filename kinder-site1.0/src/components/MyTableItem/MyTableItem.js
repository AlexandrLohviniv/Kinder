import React from 'react';
import DropdownRightButton from '../Dropdown-right-button';


const MyTableItem = ({user}) => {
    const {id, firstName, lastName, role, nickName, email} = user;
    return(
            <tr>
                <td>{id}</td>
                <td>{firstName}</td>
                <td>{lastName}</td>
                <td>{nickName}</td>
                <td>{email}</td>
                <td>
                    <DropdownRightButton title={role} direction='right'/>
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


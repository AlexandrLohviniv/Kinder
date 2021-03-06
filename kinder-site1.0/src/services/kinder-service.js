export default class KinderService {
        _apiBase = 'http://localhost:5000';


    async getResourse(url, method) {

        const token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJyb2xlIjoiU2ltcGxlVXNlciIsIm5iZiI6MTYyMTg0NjE0MiwiZXhwIjoxNjIxOTMyNTQyLCJpYXQiOjE2MjE4NDYxNDJ9.RMCCwEeD4_UHty9YK53LFU_AC5ScDd5JkqQPltaLlLs";
        const myHeaders = new Headers();
        myHeaders.append("Authorization", `Bearer ${token}`);

        const requestOptions = {
        method: method,
        headers: myHeaders,
        redirect: 'follow'
};

        const res = await fetch(`${this._apiBase}${url}`, requestOptions);
        if(!res.ok) {
            console.log(`ERROR at ${this._apiBase}${url}. RESPONSE STATUS: ${res.status}`);
        }

        return await res.json();
    }


    async getAllUsers(){
        const res = await this.getResourse(`/MatchPage/users/`, 'GET');
        res.map(user => this._transformUser(user));
        return res;
    }


    async _transformUser(user) {
        
        switch(user.role) {
            case 0:
                user.role = 'SIMPLE';
                break;
            case 1:
                user.role = 'ADMIN';
                break;
            default:
                break;
        }
        return user;
    }
}
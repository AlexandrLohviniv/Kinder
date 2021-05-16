export default class KinderService {
        _apiBase = 'http://localhost:3000';


    async getResourse(url) {

        const res = await fetch(`${this._apiBase}${url}`);
        if(!res.ok) {
            console.log(`ERROR at ${this._apiBase}${url}. RESPONSE STATUS: ${res.status}`);
        }

        return await res.json();
    }


    async getAllUsers(){
        return await this.getResourse(`/users/`);
    }
}
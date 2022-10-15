import axios  from "axios";

const baseUrl = process.env.VUE_APP_API_URL

export const ger3ahApi = {
    getNumberOfNameLeft: () => {
       return axios.get(`${baseUrl}/api/Ger3ah/GetAllGer3ahNames`);
    },
    pickAName: (name, email) => {
        return axios.get(`${baseUrl}/api/Ger3ah/NamePicker?name=${name}&email=${email}`)
    },
    getGer3ahHestory: (name) => {
        return axios.get(`${baseUrl}/api/Ger3ah/GetGer3ahHestory?name=${name}`)
    },
    reBuildTheGer3ah:() => {
        return axios.get(`${baseUrl}/api/Ger3ah/ReBuildTheGer3ah`)
    },
    removeNameFromGer3ah:(nameThatWillRemoved) => {
        return axios.get(`${baseUrl}/api/Ger3ah/RemoveNameFromGer3ah?name=${nameThatWillRemoved}`)
    }
};
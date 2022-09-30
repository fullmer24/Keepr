import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class KeepsService {
    async getKeeps() {
        const res = await api.get('api/keeps')
        AppState.keeps = res.data
    }
    async getById(id) {
        const res = await api.get(`api/keeps/${id}`)
        AppState.activeKeep = res.data
    }
    async deleteKeep(id) {
        const res = await api.delete(`api/keeps/${id}`)
        logger.log('deleting keep from service', res.data)
        AppState.keeps = AppState.keeps.filter(k => k.id != id)
    }
    async getAccountKeeps(id) {
        const res = await api.get(`api/profiles/${id}/keeps`)
        logger.log(res.data, 'getting keeps for profile')
        AppState.myKeeps = res.data
        logger.log('appstate', AppState.myKeeps)
    }
    async getKeepsByProfileId(id) {
        console.log('am i hitting this?');
        const res = await api.get(`api/profiles/${id}/keeps`)
        logger.log(res.data, 'getting keeps for profile')
        AppState.profileKeeps = res.data
        logger.log('appstate', AppState.keeps)
    }

}


export const keepsService = new KeepsService()
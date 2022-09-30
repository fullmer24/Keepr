import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class VaultsService {
    async getVaults() {
        const res = await api.get('api/vaults')
        logger.log(res.data, 'getting vaults')
        AppState.vaults = res.data
        logger.log('appstate', AppState.vaults)
    }

    async getAccountVaults() {
        const res = await api.get('account/vaults')
        // logger.log(res.data, 'getting vaults for profile')

        AppState.myVaults = res.data
        // logger.log('account vaults', AppState.myVaults)
    }

    // get vaults by profile
    async getVaultsByProfileId(id) {
        const res = await api.get(`api/profile/${id}/vaults`)
        // logger.log(res.data, 'getting vaults for profile')
        AppState.vaults = res.data
        // logger.log('appstate', AppState.vaults)
    }


}

export const vaultsService = new VaultsService()
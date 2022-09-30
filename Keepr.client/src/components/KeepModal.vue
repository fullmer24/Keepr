<template>
    <div class="modal fade" id="keepModal" tabindex="-1" aria-labelledby="keepModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header ">
                    <button title="close" type="button" class="btn-close" data-bs-dismiss="modal"
                        aria-label="Close"></button>
                </div>
                <div class="modal-body ">
                    <div class="row">
                        <div class="col-6">
                            <img class="img-fluid" :src="keep?.img" alt="">
                        </div>
                        <div class="col-6 text-dark">
                            <div>
                                <span title="views"><i class="mdi mdi-eye"></i>{{keep?.views}}</span>
                            </div>
                            <div>
                                <span title="kept"><i class="mdi mdi-safe"></i>{{keep?.kept}}</span>
                            </div>
                            <div class="text-center">
                                <div class="pt-2">
                                    <h1 class="">{{keep?.name}}</h1>
                                </div>
                                <div class="pt-2">
                                    <h1 class="border-bottom ">{{keep?.description}}</h1>
                                </div>
                            </div>
                            <div class="row">
                                <!-- NOTE use for Vault selection -->
                                <!-- <router-link v-if="keep" :to="{name: 'Vault Page', params: {id:keep?.id}}">
                                        <button type="button" data-bs-dismiss="modal" class="btn btn-primary">See Details</button>
                                    </router-link> -->
                                <div v-if="user.isAuthenticated" title="add to vault" class="col-4 p-2 ms-md-4">
                                    <div class="dropdown">

                                        <button class="btn btn-secondary dropdown-toggle" type="button"
                                            id="dropdownMenuButton1" data-bs-toggle="dropdown" aria-expanded="false">
                                            Add to Vault
                                        </button>
                                        <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">
                                            <template v-for="v in vaults" :key="v.id">
                                                <li><a class="dropdown-item">{{ v.name }}</a></li>
                                            </template>
                                        </ul>
                                    </div>
                                </div>
                                <div title="delete keep" class="col-2 p-2 ms-4 ms-md-5"
                                    v-if="account.id == keep?.creatorId">
                                    <button @click="deleteKeep(keep.id)" data-bs-dismiss="modal"><i
                                            class="mdi mdi-delete p-s"></i></button>
                                </div>
                                <div title="view profile" class="col-2 p-2 ms-3 ms-md-5">
                                    <!-- NOTE finish profile link -->
                                    <img @click="goToProfile(keep?.creator?.id)" class="selectable creator-img p-s"
                                        :src="keep?.creator.picture" data-bs-dismiss="modal" alt="">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import { keepsService } from '../services/KeepsService.js';
import { computed, onMounted } from 'vue';
import { vaultsService } from '../services/VaultsService.js';
import { AppState } from '../AppState';
import { logger } from '../utils/Logger.js';
import { Modal } from 'bootstrap';
import Pop from '../utils/Pop.js';
import { router } from '../router.js';
export default {
    setup() {
        return {
            keep: computed(() => AppState.activeKeep),
            account: computed(() => AppState.account),
            user: computed(() => AppState.user),
            vaults: computed(() => AppState.myVaults),
            async deleteKeep(id) {
                try {
                    const yes = await Pop.confirm('Delete this keep?')
                    if (!yes) { return }
                    await keepsService.deleteKeep(id)
                    Pop.toast(`Keep deleted`)
                    router.push({ name: 'Home' })

                } catch (error) {
                    logger.error('Delete Error', error)
                    Pop.error(error.message)
                }
            },
            async saveToVault(vaultId, keepId) {
                try {
                    console.log(vaultId)
                    console.log(keepId)
                } catch (error) {
                    logger.error('save to vault', error)
                    Pop.error(error.message)
                }
            },
            async goToProfile(id) {
                // REVIEW syntax for router push
                router.push(`profiles/${id}`)
            }
        };
    },
};
</script>
<style scoped>
.creator-img {
    height: 35px;
    width: 35px;
}

.vaultTab {
    color: aquamarine;
    border-color: aquamarine;
}
</style>
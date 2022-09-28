<template>
    <div class="rounded elevation-4 justify-content-center selectable" @click="setActive">
        <img class="keep-img" :src="keep.img" alt="">
        <div class="p-2">
            <h1 class="text-center border-bottom">{{keep.name}}</h1>
            <img class="rounded creator-img" :src="keep.creator.picture" alt="">
        </div>
    </div>
    <KeepModal />
</template>



<script>
import { Modal } from 'bootstrap';
import { keepsService } from '../services/KeepsService.js';
import { logger } from '../utils/Logger.js';
import KeepModal from './KeepModal.vue';
export default {
    props: {
        keep: { type: Object, required: true }
    },
    setup(props) {
        return {
            async setActive() {
                try {
                    Modal.getOrCreateInstance(document.getElementById("keepModal")).toggle();
                    await keepsService.getById(props.keep.id)
                } catch (error) {
                    logger.error(error)
                }
            }
        };
    },
    components: { KeepModal }
};
</script>




<style scoped>
.keep-img {
    height: 175px;
    width: 200px;
    object-fit: cover;
}

.creator-img {
    height: 50px;
    width: 50px;
}
</style>
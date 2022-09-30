<template>
    <div class="position-relative rounded elevation-4 justify-content-center selectable" @click="setActive">
        <img class="keep-img" :src="keep.img" alt="">
        <div class="position-absolute left-0 bottom-0 d-flex justify-content-between w-100">
            <div class="p-2 col-6 col-md-9">
                <h3 class="border-bottom mt-2 offset-1">{{keep.name}}</h3>
            </div>
            <div class="p-2 col-6 col-md-3">
                <img class="rounded creator-img mt-3 mt-md-1" :src="keep.creator.picture" alt="">
            </div>
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
    width: 100px;
    object-fit: cover;
}

.creator-img {
    height: 40px;
    width: 40px;
}
</style>
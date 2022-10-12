<template>
  <div class="about">
    <div v-if="!islogedIn" class="login">
      <h1>كلمة المرور</h1>
      <input
        v-model="pass"
        @keypress.enter="log()"
        type="password"
        style="
          width: 250px;
          border-radius: 50px;
          text-align: center;
          font-size: 20px;
        "
      />
      <br />
      <button
        style="width: 150px; border-radius: 50px"
        type="button"
        class="btn btn-outline-dark mt-4 mb-4"
        @click="log()"
      >
        دخول
      </button>
      <br />
      {{ msg }}
    </div>
    <div v-else>
      <div style="margin-bottom: -52px;">
        <button
          style="width: 150px; border-radius: 50px"
          type="button"
          class="btn btn-outline-danger mt-4 mb-4"
          @click="show_remove_name_soft_module()"
        >
          اقصاء
        </button>
      </div>
      <br />
      <div>
        <button
          style="width: 252px; border-radius: 50px"
          type="button"
          class="btn btn-outline-danger mt-4 mb-4"
          @click="show_AddOrRemoveNames()"
        >
          اضافة اسم او ازالة اسم
        </button>
      </div>
      <br />
      <div>
        <p style="margin-bottom: -6px">
          إعادة القرعة بكل الاسماء الموجودة فيها
        </p>
        <button
          style="width: 150px; border-radius: 50px"
          type="button"
          class="btn btn-outline-warning mt-1 mb-4"
          @click="showReger3ahModel()"
        >
          إعادة
        </button>
      </div>
      <br />
      {{ msg }}
      <div>
        <b-modal
          id="modal-center"
          ref="addOrRemoveNames"
          centered
          title="تأكيد"
          hide-footer
        >
          <p class="moo my-4">اضافة اسم للقرعة او ازالة اسم منها</p>
          <div class="Speasing">
          <h3>ادخل اسم</h3>
          <input
            style="
              width: 250px;
              border-radius: 50px;
              text-align: center;
              font-size: 20px;
            "
            v-model="nameThatWillRemoved"
            type="text"
          />
          <br>
          {{ msg }}
          </div>
          <b-button
            class="mt-2"
            variant="outline-warning"
            block
            @click="adinnnggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggg()"
            >إضافة</b-button
          >
          <b-button
            class="mt-2"
            variant="outline-warning"
            block
            @click="removinggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggg()"
            >إزالة</b-button
          >
          <b-button
            class="mt-3"
            variant="outline-danger"
            block
            @click="hide_AddOrRemoveNames()"
            >إلغاء</b-button
          >
        </b-modal>
      </div>
      <div>
        <b-modal
          id="modal-center"
          ref="removeNameSoft"
          centered
          title="تأكيد"
          hide-footer
        >
          <p class="moo my-4">
             هنا يمكنك ازالة اسم بحيث يكون كأن الشخص المطلوب اختار شخص وشخص اختارة لكن ما راح يتغير فيه شي
          </p>
          <div class="Speasing">
            <p>ادخل اسم</p>
          <input
            style="
              width: 250px;
              border-radius: 50px;
              text-align: center;
              font-size: 20px;
            "
            v-model="nameThatWillRemoved"
            type="text"
          />
          <br>
          {{ msg }}
          </div>
          <b-button
            class="mt-2"
            variant="outline-warning"
            block
            @click="removeName()"
            >تأكيد وتابع</b-button
          >
          <b-button
            class="mt-3"
            variant="outline-danger"
            block
            @click="hide_remove_name_soft_module"
            >إلغاء</b-button
          >
        </b-modal>
      </div>
      <div>
        <b-modal
          id="modal-center"
          ref="reger3ahModel"
          centered
          title="تأكيد"
          hide-footer
        >
          <p class="moo my-4">هل انت مأكد من أنك تريد اعادة القرعة</p>
          <b-button
            class="mt-2"
            variant="outline-warning"
            block
            @click="reGer3ah()"
            >نعم</b-button
          >
          <b-button
            class="mt-3"
            variant="outline-danger"
            block
            @click="hideReger3ahModel"
            >لا</b-button
          >
        </b-modal>
      </div>
    </div>
  </div>
</template>
  <script>
import { ger3ahApi } from "../Services/Ger3ahService";
export default {
  data() {
    return {
      nameThatWillRemoved: "",
      pass: "",
      msg: "",
      islogedIn: false,
    };
  },
  methods: {
    log() {
      this.msg = "";
      if (this.pass != "Fahad") {
        this.msg = "كملة المرور خطأ";
        return;
      }
      this.islogedIn = true;
    },
    reGer3ah() {
      this.msg = "";
      ger3ahApi
        .reBuildTheGer3ah()
        .then(() => {
          this.msg = "تمت بنجاح";
        })
        .catch(() => {
          this.msg = "حصل خطأ";
        });
      this.hideReger3ahModel();
    },
    removeName() {
      this.msg = "";
      ger3ahApi
        .removeNameFromGer3ah(this.nameThatWillRemoved)
        .then((res) => {
          if (
            res.data.status ==
            "the name that was entered  does not exist in the system"
          ) {
            this.msg = "الاسم المدخل غير موجود";
          }
          if (res.data.status == "Someone picked that name") {
            this.msg = "فيه احد قد اختار الشخص هذا";
          }
          if (res.data.status == "that name is Already piced a name") {
            this.msg = "هذا الشخص قد اختار اسم";
          }
          if (res.data.status == "Done") {
            this.msg = "تمت إزالة الاسم بنجاح";
          }
        })
        .catch(() => {
          this.msg = "حصل خطأ";
        });
    },
    addOrDelateName() {},
    show_AddOrRemoveNames() {
      this.$refs["addOrRemoveNames"].show();
    },
    hide_AddOrRemoveNames() {
      this.$refs["addOrRemoveNames"].hide();
    },
    show_remove_name_soft_module() {
      this.$refs["removeNameSoft"].show();
    },
    hide_remove_name_soft_module() {
      this.$refs["removeNameSoft"].hide();
    },
    showReger3ahModel() {
      this.$refs["reger3ahModel"].show();
    },
    hideReger3ahModel() {
      this.$refs["reger3ahModel"].hide();
    },
  },
};
</script>

<style scoped>
.title {
  position: absolute;
  top: -161px;
}
.moo {
  text-align: right !important;
}
.Speasing{
    text-align: center;
    margin: 54px 0;
}
</style>
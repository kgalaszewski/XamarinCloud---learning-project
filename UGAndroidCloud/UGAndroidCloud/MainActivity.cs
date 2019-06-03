using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Firebase.Firestore;
using Firebase;
using Java.Util;
using Android.Content;
using System.Threading.Tasks;
using Android.Gms.Tasks;
using Java.Lang;
using UGAndroidCloud.Models;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using UGAndroidCloud.Adapters;

namespace UGAndroidCloud
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, IOnSuccessListener
    {
        EditText Type;
        EditText Breed;
        EditText Name;
        EditText Info;
        //EditText AllInfo;
        Button saveButton;
        //Button refreshButton;
        RecyclerView recyclerView;

        DataAdapter dataAdapter;

        FirebaseFirestore database;
        List<InfoModel> listOfInfos = new List<InfoModel>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            ConnectViews();
            database = GetDataBase(); // z tym moze byc problem, musialem deklarowac typ, powinien byc wczesniej?
            //AllInfo.Text = GetAllCurrentInfo();

            FetchData();
            SetuprecyclerView();
        }

        void ConnectViews()
        {
            Type = (EditText)FindViewById(Resource.Id.Type);
            Breed = (EditText)FindViewById(Resource.Id.Breed);
            Name = (EditText)FindViewById(Resource.Id.Name);
            Info = (EditText)FindViewById(Resource.Id.Info);
            //AllInfo = (EditText)FindViewById(Resource.Id.AllInfo);
            saveButton = (Button)FindViewById(Resource.Id.saveButton);
            //refreshButton = (Button)FindViewById(Resource.Id.refreshButton);
            recyclerView = (RecyclerView)FindViewById(Resource.Id.dataRecyclerView);
            
            saveButton.Click += SaveButton_Click;
           // refreshButton.Click += RefreshButton_Click;
        }

        public string GetAllCurrentInfo()
        {
            return "";
        }

        void FetchData()
        {
            database.Collection("Infos").Get().AddOnSuccessListener(this);
        }

        void SetuprecyclerView()
        {
            recyclerView.SetLayoutManager(new LinearLayoutManager(recyclerView.Context));
            dataAdapter = new DataAdapter(listOfInfos);
            recyclerView.SetAdapter(dataAdapter);
        }

        //public async Task<string> GetAllCurrentInfo()
        //{
        //    DocumentReference docRef = database.Collection("cities").Document("SF");
        //    DocumentSnapshot snapshot = await docRef.SnapshotAsync();
        //    return "";
        //}

        private void SaveButton_Click(object sender, System.EventArgs e)
        {
            HashMap map = new HashMap();
            map.Put("Type", Type.Text);
            map.Put("Breed", Breed.Text);
            map.Put("Name", Name.Text);
            map.Put("Info", Info.Text);

            DocumentReference docRef = database.Collection("Infos").Document();
            docRef.Set(map);
        }

        private void RefreshButton_Click(object sender, System.EventArgs e)
        {
            //AllInfo.Text = GetAllCurrentInfo();
        }

        public FirebaseFirestore GetDataBase()
        {
            FirebaseFirestore database;

            var options = new FirebaseOptions.Builder().SetProjectId("ugandroidcloudwikiprojectt")
                .SetApplicationId("ugandroidcloudwikiprojectt")
                .SetApiKey("AIzaSyCBm-SJ2o0O-CmpdsaIwwKlkVnx3zAYwRM")
                .SetDatabaseUrl("https://ugandroidcloudwikiprojectt.firebaseio.com")
                .SetStorageBucket("ugandroidcloudwikiprojectt.appspot.com").
                Build();

            var app = FirebaseApp.InitializeApp(this, options);
            database = FirebaseFirestore.GetInstance(app);

            return database;
        }

        public void OnSuccess(Object result)
        {
            var snapshot = (QuerySnapshot)result;

            if(!snapshot.IsEmpty)
            {
                var documents = snapshot.Documents;

                listOfInfos.Clear();
                foreach (var item in documents)
                {
                    InfoModel info = new InfoModel();

                    info.Type = item.Get("Type").ToString();
                    info.Breed = item.Get("Breed").ToString();
                    info.Name = item.Get("Name").ToString();
                    info.Info = item.Get("Info").ToString();

                    listOfInfos.Add(info);
                }
            }
        }
    }
}
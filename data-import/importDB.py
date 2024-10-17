from pymongo.mongo_client import MongoClient
from pymongo.server_api import ServerApi
import pandas as pd
import json

# MongoDB URL
mongo_url = "mongodb+srv://jstar:jeH69A2jQMSPY1iG@jstar0.y0l88.mongodb.net/dotNet_exp?retryWrites=true&w=majority&appName=dotNet-exp"

# Connect to MongoDB
client = MongoClient(mongo_url, server_api=ServerApi('1'))

# Data Structure
""" export class Course extends Document implements ICourse {
    @Prop()
    id: number;

    @Prop()
    name: string;

    @Prop()
    qualification: 'undergraduate' | 'bachelor' | 'doctor';

    @Prop()
    major?: string;

    @Prop()
    description?: string;
} """

# 批量生成 Test_data 针对 不同的 qualification 和 major（cs, math, physics）
test_data = []
for i in range(2, 11):
        test_data.append({
                "id": i,
                "name": f"Course_{i}",
                "qualification": "undergraduate",
                "major": "cs",
                "description": f"Description of Course_{i}"
        })

for i in range(11, 21):
        test_data.append({
                "id": i,
                "name": f"Course_{i}",
                "qualification": "bachelor",
                "major": "math",
                "description": f"Description of Course_{i}"
        })

for i in range(21, 31):
        test_data.append({
                "id": i,
                "name": f"Course_{i}",
                "qualification": "doctor",
                "major": "physics",
                "description": f"Description of Course_{i}"
        })

for i in range(31, 41):
        test_data.append({
                "id": i,
                "name": f"Course_{i}",
                "qualification": "undergraduate",
                "major": "common",
                "description": f"Description of Course_{i}"
        })

# Convert test data to JSON
test_data = json.loads(json.dumps(test_data))

# Insert data into MongoDB
db = client.dotNet_exp
collection = db.courses
collection.insert_many(test_data)

print("Test data inserted successfully.")
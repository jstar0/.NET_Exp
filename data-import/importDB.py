from pymongo.mongo_client import MongoClient
from pymongo.server_api import ServerApi
import pandas as pd
import json

# MongoDB URL
mongo_url = "mongodb+srv://jstar:O3v9V0dvadAqHZ7q@jstar0.y0l88.mongodb.net/dotNet_exp3?retryWrites=true&w=majority&appName=dotNet-exp3"

# Connect to MongoDB
client = MongoClient(mongo_url, server_api=ServerApi('1'))

# Data Structure
""" export class Course extends Document implements ICourse {
    @Prop()
    id: number;

    @Prop()
    name: string;

    @Prop()
    major?: string;

    @Prop()
    description?: string;
} """

# 批量生成 Test_data 针对 不同的 qualification 和 major（cs, math, physics）

test_data = []

test_data.append({
        "id": 1,
        "name": f"信息系统开发 (.NET)",
        "major": "cs",
        "description": f"信息系统开发的相关知识"
})

for i in range(2, 11):
        test_data.append({
                "id": i,
                "name": f"学科_{i}",
                "major": "cs",
                "description": f"Description of Course_{i}"
        })

for i in range(11, 21):
        test_data.append({
                "id": i,
                "name": f"Course_{i}",
                "major": "math",
                "description": f"Description of Course_{i}"
        })

for i in range(21, 31):
        test_data.append({
                "id": i,
                "name": f"Course_{i}",
                "major": "physics",
                "description": f"Description of Course_{i}"
        })

for i in range(31, 41):
        test_data.append({
                "id": i,
                "name": f"Course_{i}",
                "major": "common",
                "description": f"Description of Course_{i}"
        })

# Convert test data to JSON
test_data = json.loads(json.dumps(test_data))

# Insert data into MongoDB
db = client.dotNet_exp3
collection = db.courses
collection.insert_many(test_data)

print("Test data inserted successfully.")
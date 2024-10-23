from pymongo.mongo_client import MongoClient
from pymongo.server_api import ServerApi
import random
import pandas as pd
import json

# MongoDB URL
mongo_url = ""

# Connect to MongoDB
client = MongoClient(mongo_url, server_api=ServerApi('1'))

# 在 id 从 50 到 120 之间，随机生成
# id: i
# name: Course_{i}
# description: Description of Course_{i}
# 根据随机数生成下面的信息
# qualification: undergraduate, bachelor, doctor 之中随机的一个
# major: cs, math, physics 之中随机的一个

def generate_course_data(i):
    majors = ['cs', 'math', 'physics']
    
    course_data = {
        'id': i,
        'name': f'学科_{i}',
        'description': f'Description of Course_{i}',
        'major': random.choice(majors)
    }
    
    return course_data

test_data = []

for i in range(41, 121):
    test_data.append(generate_course_data(i))

# Convert test data to JSON
test_data = json.loads(json.dumps(test_data))

# Insert data into MongoDB
db = client.dotNet_exp3
collection = db.courses
collection.insert_many(test_data)

print("Test data inserted successfully.")


using InternetMall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internetmall.Models
{
    public class ListToJsonView<template>
    {
        public List<template> returnList { get; set; }
        
        public ListToJsonView(List<template> pList)
        {
            returnList = pList;
        }
    }
}

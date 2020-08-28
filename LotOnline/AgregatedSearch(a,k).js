function AgregatedSearch(a,k)
{
	var e=this;
	e.init=function()
	{
		c();
		b();
		j()
	};
	
	function c()
	{
		var m={	onGridComplete:h,getFilterData:getCommonLotFilterData};
		for(var l=0;l<a.length;l++)
		{
			new AgregatedSearchGrid(a[l].type+"Grid",a[l].url,a[l].searchDataUrl,a[l].viewDataUrl,a[l].indExternal,a[l].type,k,m).init()
		}
	}
	
	function b()
	{
		$(".content_toggler").click(function()
		{
			$(this).find(".searchResultContent").toggle();
			f($(this).find("a.link-action"))
		});
		$("a.link-action").each(function()	{	$(this).bind("click",f)	});
		$(".searchResultContent").click(function(l)	
		{
			if(l.target.tagName=="A")
			{
				document.location.href=l.target.href
			}
			return false
		});
		$("#sortColumn").change(function()
		{
			var l=g();
			for(var m=0;m<a.length;m++)
			{
				$("#"+a[m].type+"Grid").jqGrid("setGridParam",{	sortname:l.sortField,sortorder:l.sortOrder});
				i(a[m].type+"Grid")
			}
		})
	}
	
	e.applyFiltering=function()
	{
		for(var m=0;m<a.length;m++)
		{
			var l=$("#"+a[m].type+"Grid");
			l.setGridParam({	postData:getCommonLotFilterData(a[m].indExternal,a[m].type)});
			l.setGridParam({	page:1});
			$("."+a[m].type+"ShowAllLink").attr("href","#");
			i(a[m].type+"Grid")
		}
	};
	
	function f(l)
	{
		var o=$(l),m=o.parents().closest(".row").find(".field");
		var n=/icon-opened/;
		if(n.test(o.attr("class")))
		{
			m.css("display","none");
			o.removeClass("icon-opened").addClass("icon-closed")
		}
		else
		{
			m.css("display","block");
			o.removeClass("icon-closed").addClass("icon-opened")
		}
	}
	
	function i(l)
	{
		$(".data_loading_img").show();
		$(".pagination_wrapper").hide();
		$(".no_results_msg").hide();
		$("."+l+"HideableShowAllLink").hide();
		$(".searchResultContent").block(	{	message:null	});
		$("#"+l).jqGrid().trigger("reloadGrid")
	}
	
	function j()
	{
		$(".pagination_wrapper_next").bind("click",function()
		{
			var m=$(this).closest(".content_toggler").find(".gridTable");
			var n=Number($(m).jqGrid("getGridParam","lastpage"));
			var l=Number($(m).jqGrid("getGridParam","page"));
			if(l<n)
			{
				$(m).setGridParam(	{	page:l+1});
				$(m).trigger("reloadGrid")
			}
			return false
		});
		$(".pagination_wrapper_prev").bind("click",function()
		{
			var m=$(this).closest(".content_toggler").find(".gridTable");
			var l=Number($(m).jqGrid("getGridParam","page"));
			if(l>0)
			{
				$(m).setGridParam(	{	page:l-1});
				$(m).trigger("reloadGrid")
			}
			return false
		})
	}
	
	function g()
	{
		var l=$("#sortColumn option:selected").attr("id");
		return	{	sortField:l.split("_")[0],sortOrder:l.split("_")[1]	}
	}
	
	function h(p,r)
	{
		var q=$("#"+p);
		var l=q.closest(".content_toggler");
		l.find(".data_loading_img").hide();
		l.find(".pagination_wrapper").hide();
		l.find(".no_results_msg").hide();
		l.find(".searchResultContent").unblock();
		var o=q.jqGrid("getGridParam","records");
		var n=q.jqGrid("getGridParam","page");
		var m=q.jqGrid("getGridParam","rowNum");
		if(q.getGridParam("reccount")===0)
		{
			var s=	{	photoUrl:"",publicationDate:k.emptyMessage,code:""	};
			q.addRowData(0,s);
			l.find(".no_results_msg").show();
			l.find(".pagination_wrapper").hide();
			$("."+p+"HideableShowAllLink").hide()
		}
		else
		{
			l.find(".pagination_wrapper_from").text((n-1)*m+1);
			l.find(".pagination_wrapper_to").text(Math.min(n*m,o));
			l.find(".pagination_wrapper_total").text(o);
			l.find(".no_results_msg").hide();
			l.find(".pagination_wrapper").show();
			d(p,r);
			$("."+p+"HideableShowAllLink").show()
		}
		$("#"+p+"SearchCount").text(o)
	}
	
	function d(m,n)
	{
		var l=$("."+m+"ShowAllLink").attr("href");
		if(n)
		{
			l+=getCommonLotFilterDataAsUrlParams(n)
		}
		else
		{
			var o=l.indexOf("?");
			if(o!=-1)
			{
				l=l.substring(0,o)
			}
			l+="?"+getCommonLotFilterDataAsUrlParams(n)+"&useFilterParamsFromURL=true"
		}
		$("."+m+"ShowAllLink").attr("href",l)
	}
	
};
